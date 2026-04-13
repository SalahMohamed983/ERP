using ApplicationLayer.Base;
using ApplicationLayer.Common;
using ApplicationLayer.Features.AuthAndPermissions.Email.Commands.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.Email.Commands.Handlers
{
    public class SendPasswordResetHandler: BaseEmail<SendPasswordResetHandler>, IRequestHandler<SendPasswordResetCommand, Response<bool>>
    {
        private readonly ILogger<SendPasswordResetHandler> _logger;
        private readonly EmailSettingsDto _settings;
        private readonly ResponseHandler _responseHandler;

        public SendPasswordResetHandler(IOptions<EmailSettingsDto> options, ILogger<SendPasswordResetHandler> logger, ResponseHandler responseHandler)
        :base(options, logger)
        {
            _settings = options.Value;
            _logger = logger;
            _responseHandler = responseHandler;
        }

        public async Task<Response<bool>> Handle(SendPasswordResetCommand request, CancellationToken cancellationToken)
        {
            var subject = "إعادة تعيين كلمة المرور - Restaurant API";
            var body = $@"
                <div dir='rtl' style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <h2 style='color: #333;'>إعادة تعيين كلمة المرور</h2>
                    <p>لقد طلبت إعادة تعيين كلمة المرور. يرجى النقر على الرابط أدناه لإعادة تعيين كلمة المرور:</p>
                    <p style='text-align: center; margin: 30px 0;'>
                        <a href='{request.ResetLink}' 
                           style='background-color: #dc3545; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; display: inline-block;'>
                            إعادة تعيين كلمة المرور
                        </a>
                    </p>
                    <p>أو يمكنك نسخ الرابط التالي ولصقه في المتصفح:</p>
                    <p style='word-break: break-all; color: #666;'>{request.ResetLink}</p>
                    <p style='color: #999; font-size: 12px; margin-top: 30px;'>
                        إذا لم تطلب إعادة تعيين كلمة المرور، يمكنك تجاهل هذه الرسالة.
                    </p>
                </div>
                <div dir='ltr' style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <h2 style='color: #333;'>Password Reset</h2>
                    <p>You have requested to reset your password. Please click the link below to reset your password:</p>
                    <p style='text-align: center; margin: 30px 0;'>
                        <a href='{request.ResetLink}' 
                           style='background-color: #dc3545; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; display: inline-block;'>
                            Reset Password
                        </a>
                    </p>
                    <p>Or you can copy and paste the following link into your browser:</p>
                    <p style='word-break: break-all; color: #666;'>{request.ResetLink}</p>
                    <p style='color: #999; font-size: 12px; margin-top: 30px;'>
                        If you didn't request a password reset, you can ignore this email.
                    </p>
                </div>";

            var sent = await SendEmailAsync(request.Email, subject, body);
            if (sent)
                return _responseHandler.Success(true);
            else
                return _responseHandler.BadRequest<bool>("Failed to send reset email.");
        }
    }
}
