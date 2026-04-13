using ApplicationLayer.Features.AuthAndPermissions.Email.Commands.Models;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using ApplicationLayer.Base;
using ApplicationLayer.Common;

namespace ApplicationLayer.Features.AuthAndPermissions.Email.Commands.Handlers
{
    public class SendEmailConfirmationHandler: BaseEmail<SendEmailConfirmationHandler>,  IRequestHandler<SendEmailConfirmationCommand, Response<bool>>
    {
        private readonly ILogger<SendEmailConfirmationHandler> _logger;
        private readonly EmailSettingsDto _settings;
        private readonly ResponseHandler _responseHandler;

        public SendEmailConfirmationHandler(IOptions<EmailSettingsDto> options, ILogger<SendEmailConfirmationHandler> logger, ResponseHandler responseHandler)
        : base(options, logger){
            _settings = options.Value;
            _logger = logger;
            _responseHandler = responseHandler;
        }

        public async Task<Response<bool>> Handle(SendEmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            var subject = "تأكيد بريدك الإلكتروني - Restaurant API";
            var body = $@"
                <div dir='rtl' style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <h2 style='color: #333;'>مرحباً بك في Restaurant API</h2>
                    <p>شكراً لك على التسجيل! يرجى تأكيد بريدك الإلكتروني بالنقر على الرابط أدناه:</p>
                    <p style='text-align: center; margin: 30px 0;'>
                        <a href='{request.ConfirmationLink}' 
                           style='background-color: #007bff; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; display: inline-block;'>
                            تأكيد البريد الإلكتروني
                        </a>
                    </p>
                    <p>أو يمكنك نسخ الرابط التالي ولصقه في المتصفح:</p>
                    <p style='word-break: break-all; color: #666;'>{request.ConfirmationLink}</p>
                    <p style='color: #999; font-size: 12px; margin-top: 30px;'>
                        إذا لم تقم بالتسجيل في موقعنا، يمكنك تجاهل هذه الرسالة.
                    </p>
                </div>
                <div dir='ltr' style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <h2 style='color: #333;'>Welcome to Restaurant API</h2>
                    <p>Thank you for registering! Please confirm your email address by clicking the link below:</p>
                    <p style='text-align: center; margin: 30px 0;'>
                        <a href='{request.ConfirmationLink}' 
                           style='background-color: #007bff; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; display: inline-block;'>
                            Confirm Email Address
                        </a>
                    </p>
                    <p>Or you can copy and paste the following link into your browser:</p>
                    <p style='word-break: break-all; color: #666;'>{request.ConfirmationLink}</p>
                    <p style='color: #999; font-size: 12px; margin-top: 30px;'>
                        If you didn't sign up for our service, you can safely ignore this email.
                    </p>
                </div>";

            var sent = await SendEmailAsync(request.Email, subject, body);
            if (sent)
                return _responseHandler.Success(true);
            else
                return _responseHandler.BadRequest<bool>("Failed to send confirmation email.");

        }
    }
}
