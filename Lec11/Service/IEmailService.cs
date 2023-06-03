using Lec11.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lec11.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions emailOptions);
        Task SendConfirmEmail(UserEmailOptions emailOptions);
        Task SendEmail(UserEmailOptions emailOptions);
        string GetEmailBody(string templateName);
        string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs);
        Task SendForgotPasswordEmail(UserEmailOptions _emailOptions);
    }
}