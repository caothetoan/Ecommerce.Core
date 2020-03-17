using System.Threading.Tasks;

namespace Vnit.ApplicationCore.Interfaces
{

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName,
            string host, int port,
            string userName, string password, bool enableSsl, bool useDefaultCredentials);

        Task SendEmailConfirmationAsync(string userEmail, string callbackUrl);
    }
}
