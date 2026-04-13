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
    public class ForgotPasswordCommandHandler : AuthHandlerBase, IRequestHandler<ForgotPasswordCommand, Response<bool>>
    {
        private readonly ResponseHandler _responseHandler;

        public ForgotPasswordCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork uow,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IOptions<EmailSettingsDto> emailSettings,
            ILogger<ForgotPasswordCommandHandler> logger,
            IMediator mediator,
            ResponseHandler responseHandler)
            : base(userManager, uow, configuration, httpClientFactory, emailSettings, logger, mediator)
        {
            _responseHandler = responseHandler;
        }

        public async Task<Response<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.ForgotPasswordDto.Email);
            if (user == null)
            {
                // Do not reveal existence of user
                return _responseHandler.Success(true);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);
            var baseUrl = _emailSettings.BaseUrl;
            var resetLink = $"{baseUrl}?email={Uri.EscapeDataString(user.Email!)}&token={encodedToken}";

            // Use mediator to send a SendPasswordResetCommand so existing email handlers are used
            var result = await _mediator.Send(new SendPasswordResetCommand { Email = user.Email!, ResetLink = resetLink }, cancellationToken);

            if (result == null)
                return _responseHandler.BadRequest<bool>("Failed to send reset email.");

            return result;
        }
    }
}
