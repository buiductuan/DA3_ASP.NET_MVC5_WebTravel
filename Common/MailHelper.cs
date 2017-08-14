using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Common
{
   public class MailHelper
    {
        public static void SendMail(string toEmailAddress, string subject, string content)
        {
            var fromToEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smpthost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smptport = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"].ToString());

            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailPassword), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromToEmailAddress, fromEmailPassword);
            client.Host = smpthost;
            client.Port = !string.IsNullOrEmpty(smptport) ? Convert.ToInt32(smptport) : 0;

            client.Send(message);
        }
    }
}
