using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CDKTTCTN.Services.Emails
{
    public class EmailHelper
    {
        // api 8.0.5
        public static async Task<string> SendMail(
            string subject, string body,
            string fromName, string fromEmail,
            string toName, string toEmail)
        {
            if (string.IsNullOrEmpty(toEmail))
                return null;

            var apikey = ConfigurationManager.AppSettings["SendGridApiKey"] ?? "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";


            SendGridClient sg = new SendGridClient(apikey);
            EmailAddress from = new EmailAddress(fromEmail, fromName);
            EmailAddress to = new EmailAddress(toEmail, toName);
            Content content = new Content("text/html", body);

            var message = new SendGridMessage
            {
                From = @from,
                ReplyTo = to,
                Subject = subject,
                HtmlContent = body,
                //TemplateId = ""
            };

            var response = await sg.SendEmailAsync(message);


            return response.StatusCode.ToString();
        }
    }
}
