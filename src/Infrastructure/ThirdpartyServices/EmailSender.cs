using System.Net.Mail;
using System.Threading.Tasks;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.Infrastructure.ThirdpartyServices
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName,
            string host, int port,
            string userName, string password, bool enableSsl, bool useDefaultCredentials)
        {
            // TODO: Wire this up to actual email sending logic via SendGrid, local SMTP, etc.
            var message = new System.Net.Mail.MailMessage();
            var client = new System.Net.Mail.SmtpClient
            {
                EnableSsl = enableSsl,
                Host = host,
                Port = port,
                UseDefaultCredentials = useDefaultCredentials,
                Credentials =
                    new System.Net.NetworkCredential(userName, password)
            };
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            message.From = new System.Net.Mail.MailAddress(fromAddress, fromName);

            MailAddress to = new MailAddress(toAddress, toName);
            message.To.Add(to);
           
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = (body);
            client.Send(message);
            return Task.CompletedTask;
        }

        public Task SendEmailConfirmationAsync(string userEmail, string callbackUrl)
        {
            throw new System.NotImplementedException();
        }
    }
}
