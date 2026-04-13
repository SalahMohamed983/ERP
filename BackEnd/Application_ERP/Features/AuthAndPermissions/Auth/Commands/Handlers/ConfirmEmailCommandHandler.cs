using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using DominLayer.Entites.AuthAndPermissions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ApplicationLayer.Common;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Handlers
{
    public class ConfirmEmailCommandHandler : AuthHandlerBase, IRequestHandler<ConfirmEmailCommand, Response<bool>>
    {
        private readonly ResponseHandler _responseHandler;

        public ConfirmEmailCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork uow,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IOptions<EmailSettingsDto> emailSettings,
            ILogger<ConfirmEmailCommandHandler> logger,
            IMediator mediator,
            ResponseHandler responseHandler)
            : base(userManager, uow, configuration, httpClientFactory, emailSettings, logger, mediator)
        {
            _responseHandler = responseHandler;
        }

        public async Task<Response<bool>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.ConfirmEmailDto.Email);
            if (user == null) return _responseHandler.BadRequest<bool>("Email confirmation failed.");
            if (user.EmailConfirmed) return _responseHandler.Success(true);

            string decodedToken = request.ConfirmEmailDto.Token;
            try
            {
                if (decodedToken.Contains("%")) decodedToken = Uri.UnescapeDataString(decodedToken);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to decode URL-encoded token for email confirmation. Email: {Email}", request.ConfirmEmailDto.Email);
                decodedToken = request.ConfirmEmailDto.Token;
            }

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Email confirmation failed for {Email}. Errors: {Errors}", request.ConfirmEmailDto.Email, errors);
                return _responseHandler.BadRequest<bool>("Email confirmation failed.");
            }

            return _responseHandler.Success(true);
        }
    }
}
