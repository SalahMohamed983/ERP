using ApplicationLayer.Base;
using ApplicationLayer.Common;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using DominLayer.Entites.AuthAndPermissions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Handlers
{
    public class ChangePasswordCommandHandler : AuthHandlerBase, IRequestHandler<ChangePasswordCommand, Response<bool>>
    {
        private readonly ResponseHandler _responseHandler;

        public ChangePasswordCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork uow,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IOptions<EmailSettingsDto> emailSettings,
            ILogger<ChangePasswordCommandHandler> logger,
            IMediator mediator,
            ResponseHandler responseHandler)
            : base(userManager, uow, configuration, httpClientFactory, emailSettings, logger, mediator)
        {
            _responseHandler = responseHandler;
        }

        public async Task<Response<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return _responseHandler.NotFound<bool>("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, request.ChangePasswordDto.CurrentPassword, request.ChangePasswordDto.NewPassword);
            if (result.Succeeded)
            {
                user.TokenVersion++;
                await _userManager.UpdateAsync(user);

                var userTokens = await _uow.RefreshTokens.GetAllAsync(rt => rt.UserId == request.UserId && rt.RevokedOn == null);
                foreach (var token in userTokens)
                {
                    token.RevokedOn = DateTime.UtcNow;
                    token.RevokedByIp = "Password Changed";
                }

                if (userTokens.Any())
                {
                    _uow.RefreshTokens.UpdateRange(userTokens);
                    await _uow.CompleteAsync();
                }
            }

            return _responseHandler.Success(result.Succeeded);
        }
    }
}
