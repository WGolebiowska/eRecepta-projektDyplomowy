using eRecepta_projektDyplomowy.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Controllers.Services
{
    public class MailService : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly MailSettings _MailSettings = null;
        public AuthMessageSenderOptions _MailSecretSettings { get; } //Set with Secret Manager.
        public MailService(IOptions<AuthMessageSenderOptions> optionsAccessor,
                       ILogger<MailService> logger, IOptions<MailSettings> options)
        {
            _MailSecretSettings = optionsAccessor.Value;
            _logger = logger;
            _MailSettings = options.Value;
        }
        public MimeMessage PrepareEmail(string email, string subject, string htmlMessage)
        {
            MimeMessage emailMessage = new MimeMessage();
            MailboxAddress emailFrom = new MailboxAddress(_MailSettings.Name, _MailSecretSettings.ExternalMailId);
            emailMessage.From.Add(emailFrom);
            MailboxAddress emailTo = new MailboxAddress(email, email);
            emailMessage.To.Add(emailTo);
            emailMessage.Subject = subject;
            BodyBuilder emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.HtmlBody = htmlMessage;
            emailMessage.Body = emailBodyBuilder.ToMessageBody();

            return emailMessage;
        }
        public bool SendEmail(string email, string subject, string htmlMessage)
        {
            var emailMessage = PrepareEmail(email, subject, htmlMessage);
            try
            {
                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect(_MailSettings.Host, _MailSettings.Port, SecureSocketOptions.StartTls);
                emailClient.Authenticate(_MailSecretSettings.ExternalMailId, _MailSecretSettings.ExternalMailPassword);
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();
                _logger.LogInformation($"Email to {emailMessage.To} sent successfully!");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failure Email to {emailMessage.To}");
                _logger.LogError(ex.Message);
                return false;
            }
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrEmpty(_MailSecretSettings.ExternalMailPassword))
            {
                throw new Exception("Null ExternalMailPassword");
            }
            var emailMessage = PrepareEmail(email, subject, htmlMessage);
            await Execute(emailMessage);
        }
        public async Task Execute(MimeMessage emailMessage)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_MailSettings.Host, _MailSettings.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_MailSecretSettings.ExternalMailId, _MailSecretSettings.ExternalMailPassword);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                    _logger.LogInformation($"Email to {emailMessage.To} queued successfully!");
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failure Email to {emailMessage.To}");
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
