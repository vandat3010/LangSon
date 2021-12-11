using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Namek.Library.Helpers
{
    public class EmailHelper
    {
        public static bool SendMailByGoogle(string emailusername, string emailpass, string from, string to, string cc,
            string bcc, string subject, string content, out string errorMsg)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                errorMsg = "From or to email address is null or empty.";
                return false;
            }
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(from);
            cc = to;
            //foreach (var s in toList)
            //{
            //    var mail = s.Trim();
            //    if (mail == string.Empty)
            //    {
            //        continue;
            //    }
            //    mailMessage.To.Add(mail);
            //}
            var ccList = cc.Split(';');
            var i = 0;
            foreach (var s in ccList)
            {
                var mail = s.Trim();
                if (mail == string.Empty)
                    continue;
                if (i == 0)
                    mailMessage.To.Add(mail);
                else
                    mailMessage.CC.Add(mail);
                i++;
            }
            var bccList = bcc.Split(';');
            foreach (var s in bccList)
            {
                var mail = s.Trim();
                if (mail == string.Empty)
                    continue;
                mailMessage.Bcc.Add(mail);
            }
            mailMessage.Subject = subject;
            mailMessage.Body = content;
            mailMessage.IsBodyHtml = true;
            // Create the credentials to login to the gmail account associated with my custom domain
            var sendEmailsFrom = emailusername;
            var sendEmailsFromPassword = emailpass;
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(sendEmailsFrom, sendEmailsFromPassword),
                EnableSsl = true,
                Timeout = 20000,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            try
            {
                client.Send(mailMessage);
                errorMsg = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        private static void SendMail(string mailServer, int mailServerPort, string mailAccount, string mailPassword,
            string toList, string from, string ccList, string subject, string body)
        {
            var message = new MailMessage();
            var client = new SmtpClient
            {
                EnableSsl = true,
                Host = mailServer,
                Port = mailServerPort,
                UseDefaultCredentials = true,
                Credentials =
                    new NetworkCredential(mailAccount, mailPassword)
            };
            message.From = new MailAddress(from);
            message.To.Add(toList);
            if (!string.IsNullOrEmpty(ccList)) message.CC.Add(ccList);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = string.Format(body);
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(Logger.LogType.Error, "Send mail error:" + ex.Message);
                Trace.WriteLine(ex);
            }
        }
    }
}