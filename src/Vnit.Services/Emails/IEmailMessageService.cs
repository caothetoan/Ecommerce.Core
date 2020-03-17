using System.Collections.Generic;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Emails;

namespace Vnit.ApplicationCore.Services.Emails
{
    /// <summary>
    /// Email sender
    /// </summary>
    public partial interface IEmailMessageService : IBaseEntityService<EmailMessage>
    {
        //IPagedList<EmailMessage> GetPageList(string to = null,
        //    string subject = null, int count = 10, int page = 1);
        /// <summary>
        /// Sends an email
        /// </summary>        
        bool SendEmail(EmailAccount emailAccount, string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName,
             string replyToAddress = null, string replyToName = null,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null,
            string attachmentFilePath = null, string attachmentFileName = null,
            int attachedDownloadId = 0, IDictionary<string, string> headers = null);


        bool SendEmail(EmailMessage emailMessage, bool verboseErrorOnFailure = false);

        void Queue(EmailMessage emailMessage);
    }
}
