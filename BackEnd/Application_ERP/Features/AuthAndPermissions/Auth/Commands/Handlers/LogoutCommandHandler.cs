using ApplicationLayer.Base;
using ApplicationLayer.Common;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models;
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
    public class LogoutCommandHandler : AuthHandlerBase, IRequestHandler<LogoutCommand, Response<bool>>
    {
        private readonly ResponseHandler _responseHandler;

        public LogoutCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork uow,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IOptions<EmailSettingsDto> emailSettings,
            ILogger<LogoutCommandHandler> logger,
            IMediator mediator,
            ResponseHandler responseHandler)
            : base(userManager, uow, configuration, httpClientFactory, emailSettings, logger, mediator)
        {
            _responseHandler = responseHandler;
        }

        public async Task<Response<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var tokenHash = HashToken(request.RefreshToken);
            var token = await _uow.RefreshTokens.GetAsync(rt => rt.TokenHash == tokenHash && rt.RevokedOn == null);

            if (token == null)
            {
                return _responseHandler.NotFound<bool>("Refresh token not found or already revoked.");
            }

            var latestActiveToken = _uow.RefreshTokens.Query()
                .Where(rt => rt.UserId == token.UserId && rt.RevokedOn == null
                && rt.Id != token.Id && rt.ExpiresOn > DateTime.UtcNow)
                .OrderByDescending(rt => rt.CreatedOn)
                .FirstOrDefault();

            token.RevokedOn = DateTime.UtcNow;
            token.RevokedByIp = request.IpAddress;
            if (latestActiveToken != null)
            {
                token.ReplacedByTokenId = latestActiveToken.Id;
            }
            _uow.RefreshTokens.Update(token);
            await _uow.CompleteAsync();

            return _responseHandler.Success(true);
        }
    }
}
