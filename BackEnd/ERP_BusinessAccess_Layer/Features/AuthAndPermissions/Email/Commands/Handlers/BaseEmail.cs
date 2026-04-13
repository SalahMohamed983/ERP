using ApplicationLayer.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ApplicationLayer.Features.AuthAndPermissions.Email.Commands.Handlers
{
    public class BaseEmail<T>
    {
        private readonly ILogger<T> _logger;
        private readonly EmailSettingsDto _settings;
        private readonly string? _smtpHost;
        private readonly int _smtpPort;
        private readonly string? _smtpUser;
        private readonly string? _smtpPassword;
        private readonly string? _smtpFromEmail;
        private readonly string? _smtpFromName;
        private readonly bool _enableSsl;
        private readonly string? _baseUrl;

        protected BaseEmail(IOptions<EmailSettingsDto> options, ILogger<T> logger)
        {
            _settings = options.Value;
            _logger = logger;

            _smtpHost = _settings.Smtp.Host;
            _smtpPort = _settings.Smtp.Port;
            _smtpUser = _settings.Smtp.User;
            _smtpPassword = _settings.Smtp.Password;
            _smtpFromEmail = !string.IsNullOrEmpty(_settings.Smtp.FromEmail) ? _settings.Smtp.FromEmail : _smtpUser;
            _smtpFromName = !string.IsNullOrEmpty(_settings.Smtp.FromName) ? _settings.Smtp.FromName : "Resturant API";
            _enableSsl = _settings.Smtp.EnableSsl;
            _baseUrl = _settings.BaseUrl;

        }
        protected async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                // If SMTP is not configured, just log and return true (for development)
                if (string.IsNullOrEmpty(_smtpHost) || string.IsNullOrEmpty(_smtpUser))
                {
                    _logger.LogWarning("Email not configured. Would send email to {Email}: {Subject}", to, subject);
                    _logger.LogInformation("Email Body: {Body}", body);
                    return true; // Return true in development mode
                }

                using var client = new SmtpClient(_smtpHost, _smtpPort)
                {
                    EnableSsl = _enableSsl,
                    Credentials = new NetworkCredential(_smtpUser, _smtpPassword)
                };

                using var message = new MailMessage
                {
                    From = new MailAddress(_smtpFromEmail!, _smtpFromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                message.To.Add(new MailAddress(to));

                await client.SendMailAsync(message);
                _logger.LogInformation("Email sent successfully to {Email}", to);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", to);
                return false;
            }
        }


    }
}
