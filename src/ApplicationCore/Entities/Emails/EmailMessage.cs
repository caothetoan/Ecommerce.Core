using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace Vnit.ApplicationCore.Entities.Emails
{
    /// <summary>
    /// Specifies fields used for sending emails
    /// </summary>
    public class EmailMessage : BaseEntity
    {
        public string To { get; set; }

        public IList<UserInfo> Tos { get; set; }

        //public string TosSerialized
        //{
        //    get { return JsonConvert.SerializeObject(Tos); }
        //    set { Tos = JsonConvert.DeserializeObject<List<UserInfo>>(value); }
        //}

        //public IList<UserInfo> Ccs { get; set; }

        //public string CcsSerialized
        //{
        //    get { return JsonConvert.SerializeObject(Ccs); }
        //    set { Ccs = JsonConvert.DeserializeObject<List<UserInfo>>(value); }
        //}

        //public IList<UserInfo> Bccs { get; set; }

        //public string BccsSerialized
        //{
        //    get { return JsonConvert.SerializeObject(Bccs); }
        //    set { Bccs = JsonConvert.DeserializeObject<List<UserInfo>>(value); }
        //}

        public IList<UserInfo> ReplyTos { get; set; }

        //public string ReplyTosSerialized
        //{
        //    get { return JsonConvert.SerializeObject(ReplyTos); }
        //    set { ReplyTos = JsonConvert.DeserializeObject<List<UserInfo>>(value); }
        //}

        public string Subject { get; set; }

        public string EmailBody { get; set; }

        public bool IsEmailBodyHtml { get; set; }

        public IDictionary<string, string> Headers { get; set; }

        //public string HeadersSerialized => JsonConvert.SerializeObject(Headers);

        private string _attachmentSerialized = string.Empty;

      
        public EmailTemplate OriginalEmailTemplate { get; set; }

      
        public virtual EmailAccount EmailAccount { get; set; }

        public int EmailAccountId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? SendingDate { get; set; }

        public bool IsSent { get; set; }

     
        /// <summary>
        /// Specifies a user in email communication
        /// </summary>
        public class UserInfo
        {
            public string Name { get; set; }

            public string Email { get; set; }

            public UserInfo(string name, string email)
            {
                Name = name;
                Email = email;
            }
        }
    }
}
