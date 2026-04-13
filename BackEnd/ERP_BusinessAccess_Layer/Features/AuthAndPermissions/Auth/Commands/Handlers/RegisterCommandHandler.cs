using ApplicationLayer.Base;
using ApplicationLayer.Common;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models;
using ApplicationLayer.Features.AuthAndPermissions.Email.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using DominLayer.Entites.AuthAndPermissions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Handlers
{
    public class RegisterCommandHandler : AuthHandlerBase, IRequestHandler<RegisterCommand, Response<bool>>
    {
        private readonly ResponseHandler _responseHandler;

        public RegisterCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork uow,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IOptions<EmailSettingsDto> emailSettings,
            ILogger<RegisterCommandHandler> logger,
            IMediator mediator,
            ResponseHandler responseHandler)
            : base(userManager, uow, configuration, httpClientFactory, emailSettings, logger, mediator)
        {
            _responseHandler = responseHandler;
        }

        public async Task<Response<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var registerDto = request.RegisterDto;
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return _responseHandler.BadRequest<bool>("Registration failed. User already exist.");
            }

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                PhoneNumber = registerDto.PhoneNumber,
                CreatedDate = DateTime.UtcNow,
                TokenVersion = 0,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return _responseHandler.BadRequest<bool>("Registration failed. Could not create user.");
            }

            await _userManager.AddToRoleAsync(user, "User");

            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(confirmationToken);
            var baseUrl = _emailSettings.BaseUrl;
            var confirmationLink = $"{baseUrl}/?email={Uri.EscapeDataString(user.Email!)}&token={encodedToken}";

            await _mediator.Send(new SendEmailConfirmationCommand { Email = user.Email, ConfirmationLink = confirmationLink });

            return _responseHandler.Created(true);
        }
    }
}
