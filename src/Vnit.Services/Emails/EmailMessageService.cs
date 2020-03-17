using System;
using System.Collections.Generic;
using System.Net.Mail;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Emails;

namespace Vnit.ApplicationCore.Services.Emails
{
    /// <summary>
    /// Email sender
    /// </summary>
    public partial class EmailMessageService : BaseEntityService<EmailMessage>, IEmailMessageService
    {
        #region private members

        private readonly IEmailAccountService _emailAccountService;
        #endregion
        public EmailMessageService(IDataRepository<EmailMessage> dataRepository, IEmailAccountService emailAccountService ) : base(dataRepository)
        {
            _emailAccountService = emailAccountService;
        }
        //public IPagedList<EmailMessage> GetPageList(string to = null,
        //    string subject = null, int count = 10, int page = 1)
        //{
        //    var query = Repository.Get(x => true);

        //    if (!string.IsNullOrEmpty(to))
        //        query = query.Where(x => x.To == to || x.TosSerialized.Contains(to));

        //    if (!string.IsNullOrEmpty(subject))
        //        query = query.Where(x => x.Subject.Contains(subject));

        //    query = query.OrderByDescending(x => x.CreatedDate);

        //    return new PagedList<EmailMessage>(query, (page - 1), count);
        //}

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount">Email account to use</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="fromAddress">From address</param>
        /// <param name="fromName">From display name</param>
        /// <param name="toAddress">To address</param>
        /// <param name="toName">To display name</param>
        /// <param name="replyTo">ReplyTo address</param>
        /// <param name="replyToName">ReplyTo display name</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses list</param>
        /// <param name="attachmentFilePath">Attachment file path</param>
        /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
        /// <param name="attachedDownloadId">Attachment download ID (another attachedment)</param>
        /// <param name="headers">Headers</param>
        public virtual bool SendEmail(EmailAccount emailAccount, string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName,
             string replyTo = null, string replyToName = null,
            IEnumerable<string> bcc = null, IEnumerable<string> ccList = null,
            string attachmentFilePath = null, string attachmentFileName = null,
            int attachedDownloadId = 0, IDictionary<string, string> headers = null)
        {
            try
            {
                //_logService.InsertLog(LogLevel.Information, "Bắt đầu gửi mail cho " + toAddress);

                var message = new System.Net.Mail.MailMessage();
                var client = new System.Net.Mail.SmtpClient
                {
                    EnableSsl = emailAccount.EnableSsl,
                    Host = emailAccount.Host,
                    Port = emailAccount.Port,
                    UseDefaultCredentials = emailAccount.UseDefaultCredentials,
                    Credentials =
                        new System.Net.NetworkCredential(emailAccount.Username, emailAccount.Password)
                };
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                message.From = new System.Net.Mail.MailAddress(fromAddress, fromName);

                MailAddress to = new MailAddress(toAddress, toName);
                message.To.Add(to);
                if (ccList != null)
                    foreach (var cc in ccList)
                    {
                        message.CC.Add(cc);
                    }
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = (body);
                client.Send(message);
                //_logService.InsertLog(LogLevel.Information, "Đã gửi mail");
                return true;
            }
            catch (Exception)
            {
                //_logService.InsertLog(LogLevel.Error, e.Message);
            }
            return false;
        }


        public void Queue(EmailMessage emailMessage)
        {
            Insert(emailMessage);

            if (SendEmail(emailMessage))
            {
                emailMessage.IsSent = true;
                emailMessage.SendingDate = DateTime.Now;
                Update(emailMessage);
            }
        }

        public bool SendEmail(EmailMessage emailMessage, bool verboseErrorOnFailure = false)
        {
            var emailAccount = emailMessage.EmailAccount ?? _emailAccountService.FirstOrDefault(x => x.IsDefault == true);//x => x.IsDefault
            if (emailAccount == null)
            {
                throw new Exception("Can't send email without account");
                //return false; //can't send email without account
            }

            if (emailMessage.Tos == null )
            {
                throw new Exception("At least one of Tos, CCs or BCCs must be specified to send email");
            }
            bool response = false;
            if (emailMessage.Tos != null)
            {
                foreach (var userInfo in emailMessage.Tos)
                {
                    response = SendEmail(emailAccount,
                        emailMessage.Subject,
                        emailMessage.EmailBody,
                        emailAccount.Username,
                        emailAccount.DisplayName,
                        userInfo.Email,
                        userInfo.Name);
                }
            }

            return response;
        }

    }
}
