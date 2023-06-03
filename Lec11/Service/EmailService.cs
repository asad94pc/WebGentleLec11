using Lec11.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Lec11.Service
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;
        
        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
            
        }

        public async Task SendTestEmail(UserEmailOptions _emailOptions)
        {
            _emailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}} This is Test Email", _emailOptions.PlaceHolders);
            _emailOptions.Body = UpdatePlaceHolders(GetEmailBody("htmlpage"), _emailOptions.PlaceHolders);

            await SendEmail(_emailOptions);

        }
        public async Task SendConfirmEmail(UserEmailOptions _emailOptions)
        {
            _emailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}} This is confirm Email", _emailOptions.PlaceHolders);
            _emailOptions.Body = UpdatePlaceHolders(GetEmailBody("ConfirmEmail"), _emailOptions.PlaceHolders);

            await SendEmail(_emailOptions);

        }
        public async Task SendForgotPasswordEmail(UserEmailOptions _emailOptions)
        {
            _emailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}} This is password reset Email", _emailOptions.PlaceHolders);
            _emailOptions.Body = UpdatePlaceHolders(GetEmailBody("ForgotEmail"), _emailOptions.PlaceHolders);

            await SendEmail(_emailOptions);

        }

        public async Task SendEmail(UserEmailOptions _emailOptions)
        {
            MailMessage message = new MailMessage()
            {
                Subject = _emailOptions.Subject,
                Body = _emailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddres, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            foreach (var email in _emailOptions.ToEmails)
            {
                message.To.Add(email);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient()
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            message.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(message);
        }

        public string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }

        public string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var pair in keyValuePairs)
                {
                    if (text.Contains(pair.Key))
                    {
                        text = text.Replace(pair.Key, pair.Value);
                    }
                }

            }
            return text;

        }

    }
}
