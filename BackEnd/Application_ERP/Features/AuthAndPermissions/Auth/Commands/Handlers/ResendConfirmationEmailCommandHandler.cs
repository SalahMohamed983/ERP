using ApplicationLayer.Base;
using ApplicationLayer.Common;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos;
using ApplicationLayer.Features.AuthAndPermissions.Email.Commands.Models;
using DominLayer.Entites.AuthAndPermissions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Handlers
{
    public class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand, Response<bool>>
    {
        private readonly IMediator _mediator;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly EmailSettingsDto _emailSettings;
        private readonly ResponseHandler _responseHandler;

        public ResendConfirmationEmailCommandHandler(IMediator mediator,
            UserManager<ApplicationUser> userManager,
                        IOptions<EmailSettingsDto> emailSettings,
                        ResponseHandler responseHandler)
        {
            _emailSettings = emailSettings.Value;
            _userManager = userManager;
            _mediator = mediator;
            _responseHandler = responseHandler;
        }

        public async Task<Response<bool>> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ResendConfirmationEmailDto;
            var user = await _userManager.FindByEmailAsync(request.ResendConfirmationEmailDto.Email);

            if (user == null)
            {
                // Do not reveal existence; return success
                return _responseHandler.Success(true);
            }

            if (user.EmailConfirmed)
            {
                return _responseHandler.Success(true); // Email already confirmed
            }

            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(confirmationToken);

            var baseUrl = _emailSettings.BaseUrl;
            var confirmationLink = $"{baseUrl}/?email={Uri.EscapeDataString(request.ResendConfirmationEmailDto.Email!)}&token={encodedToken}";

            var emailResponse = await _mediator.Send(new SendEmailConfirmationCommand { Email = dto.Email, ConfirmationLink = confirmationLink }, cancellationToken);
            // emailResponse is Response<bool>, return it (or wrap)
            if (emailResponse == null) return _responseHandler.BadRequest<bool>("Failed to send confirmation email.");
            return emailResponse;
        }
    }
}
