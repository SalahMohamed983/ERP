using ApplicationLayer.Base;
using ApplicationLayer.Common;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos;
using ApplicationLayer.RepoInterfaces;
using DominLayer.Entites.AuthAndPermissions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Handlers
{
    public class LoginCommandHandler : AuthHandlerBase, IRequestHandler<LoginCommand, Response<AuthResponseDto?>>
    {
        private readonly ResponseHandler _responseHandler;

        public LoginCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork uow,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IOptions<EmailSettingsDto> emailSettings,
            ILogger<LoginCommandHandler> logger,
            IMediator mediator,
            ResponseHandler responseHandler)
            : base(userManager, uow, configuration, httpClientFactory, emailSettings, logger, mediator)
        {
            _responseHandler = responseHandler;
        }

        public async Task<Response<AuthResponseDto?>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loginDto = request.LoginDto;
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return _responseHandler.Unauthorized<AuthResponseDto?>("Invalid email or password, or email not confirmed.");
            }

            if (!user.EmailConfirmed)
            {
                return _responseHandler.Unauthorized<AuthResponseDto?>("Email not confirmed.");
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isValidPassword)
            {
                return _responseHandler.Unauthorized<AuthResponseDto?>("Invalid email or password, or email not confirmed.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = await GenerateJwtTokenAsync(user, roles);

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var jwtId = jwtSecurityToken.Id;

            var refreshToken = await GenerateRefreshTokenAsync(user.Id, request.IpAddress, request.UserAgent, jwtId);

            var authResponse = new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationMinutes"] ?? "60")),
                User = new UserInfoDto
                {
                    Id = user.Id,
                    Email = user.Email!,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Roles = roles
                }
            };

            return _responseHandler.Success(authResponse);
        }
    }
}
