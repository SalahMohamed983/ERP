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
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ApplicationLayer.Common;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Handlers
{
    public class ResetPasswordCommandHandler : AuthHandlerBase, IRequestHandler<ResetPasswordCommand, Response<bool>>
    {
        private readonly ResponseHandler _responseHandler;

        public ResetPasswordCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork uow,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IOptions<EmailSettingsDto> emailSettings,
            ILogger<ResetPasswordCommandHandler> logger,
            IMediator mediator,
            ResponseHandler responseHandler)
            : base(userManager, uow, configuration, httpClientFactory, emailSettings, logger, mediator)
        {
            _responseHandler = responseHandler;
        }

        public async Task<Response<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.ResetPasswordDto.Email);
            if (user == null)
            {
                _logger.LogWarning("Password reset attempt for non-existent or deleted user: {Email}", request.ResetPasswordDto.Email);
                return _responseHandler.BadRequest<bool>("Failed to reset password. Token may be invalid or expired.");
            }

            string decodedToken = request.ResetPasswordDto.Token;
            try
            {
                if (decodedToken.Contains("%")) decodedToken = Uri.UnescapeDataString(decodedToken);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to decode URL-encoded token for password reset. Email: {Email}", request.ResetPasswordDto.Email);
                decodedToken = request.ResetPasswordDto.Token;
            }

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.ResetPasswordDto.NewPassword);
            if (result.Succeeded)
            {
                user.TokenVersion++;
                await _userManager.UpdateAsync(user);

                var userTokens = await _uow.RefreshTokens.GetAllAsync(rt => rt.UserId == user.Id && rt.RevokedOn == null);
                foreach (var token in userTokens)
                {
                    token.RevokedOn = DateTime.UtcNow;
                    token.RevokedByIp = "Password Reset";
                }

                if (userTokens.Any())
                {
                    _uow.RefreshTokens.UpdateRange(userTokens);
                    await _uow.CompleteAsync();
                }

                _logger.LogInformation("Password reset successful for user: {Email}", request.ResetPasswordDto.Email);
                return _responseHandler.Success(true);
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Password reset failed for user {Email}. Errors: {Errors}", request.ResetPasswordDto.Email, errors);
                return _responseHandler.BadRequest<bool>("Failed to reset password. Token may be invalid or expired.");
            }
        }
    }
}
