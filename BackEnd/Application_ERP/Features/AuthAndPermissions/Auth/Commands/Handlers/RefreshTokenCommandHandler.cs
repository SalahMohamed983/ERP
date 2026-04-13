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
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Handlers
{
    public class RefreshTokenCommandHandler : AuthHandlerBase, IRequestHandler<RefreshTokenCommand, Response<AuthResponseDto?>>
    {
        private readonly ResponseHandler _responseHandler;

        public RefreshTokenCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork uow,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IOptions<EmailSettingsDto> emailSettings,
            ILogger<RefreshTokenCommandHandler> logger,
            IMediator mediator,
            ResponseHandler responseHandler)
            : base(userManager, uow, configuration, httpClientFactory, emailSettings, logger, mediator)
        {
            _responseHandler = responseHandler;
        }

        public async Task<Response<AuthResponseDto?>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshDto = request.RefreshTokenRequestDto;

            var principal = GetPrincipalFromExpiredToken(refreshDto.Token);
            if (principal == null)
                return _responseHandler.Unauthorized<AuthResponseDto?>("Invalid token.");

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return _responseHandler.Unauthorized<AuthResponseDto?>("Invalid token.");

            var user = await _userManager.FindByIdAsync(userIdClaim);
            if (user == null)
                return _responseHandler.Unauthorized<AuthResponseDto?>("Invalid token.");

            var tokenVersionClaim = principal.FindFirst("TokenVersion")?.Value;
            if (string.IsNullOrEmpty(tokenVersionClaim) || !int.TryParse(tokenVersionClaim, out var tokenVersion) || tokenVersion != user.TokenVersion)
                return _responseHandler.Unauthorized<AuthResponseDto?>("Invalid token.");

            var jtiClaim = principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            if (string.IsNullOrEmpty(jtiClaim))
                return _responseHandler.Unauthorized<AuthResponseDto?>("Invalid token.");

            var tokenHash = HashToken(refreshDto.RefreshToken);
            var userRefreshToken = await _uow.RefreshTokens.GetAsync(rt => rt.UserId == userId &&
                                         rt.TokenHash == tokenHash &&
                                         rt.JwtId == jtiClaim &&
                                         rt.ExpiresOn > DateTime.UtcNow &&
                                         rt.RevokedOn == null);

            if (userRefreshToken == null)
                return _responseHandler.Unauthorized<AuthResponseDto?>("Invalid refresh token.");

            var roles = await _userManager.GetRolesAsync(user);
            var newToken = await GenerateJwtTokenAsync(user, roles);

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(newToken);
            var jwtId = jwtSecurityToken.Id;

            var randomNumber = new byte[64];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var newRefreshTokenString = Convert.ToBase64String(randomNumber);

            var newRefreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                TokenHash = HashToken(newRefreshTokenString),
                JwtId = jwtId,
                ExpiresOn = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenExpirationDays"] ?? "7")),
                CreatedOn = DateTime.UtcNow,
                CreatedByIp = request.IpAddress,
                UserAgent = request.UserAgent
            };

            await _uow.RefreshTokens.AddAsync(newRefreshTokenEntity);

            userRefreshToken.RevokedOn = DateTime.UtcNow;
            userRefreshToken.RevokedByIp = request.IpAddress;
            userRefreshToken.ReplacedByTokenId = newRefreshTokenEntity.Id;
            _uow.RefreshTokens.Update(userRefreshToken);

            await _uow.CompleteAsync();

            var authResponse = new AuthResponseDto
            {
                Token = newToken,
                RefreshToken = newRefreshTokenString,
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
