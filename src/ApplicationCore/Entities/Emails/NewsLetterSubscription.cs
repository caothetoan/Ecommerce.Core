using System;
using System.Collections.Generic;
using System.Text;
using Vnit.ApplicationCore.Attributes;

namespace Vnit.ApplicationCore.Entities.Emails
{
    /// <summary>
    /// Represents NewsLetterSubscription entity
    /// </summary>
    public partial class NewsLetterSubscription : BaseEntity
    {
        public NewsLetterSubscription()
        {
            Active = false;
            NewsLetterSubscriptionGuid = Guid.NewGuid();
        }
        #region propertises
        /// <summary>
        /// Gets or sets the newsletter subscription GUID
        /// </summary>
        public Guid NewsLetterSubscriptionGuid { get; set; }

        /// <summary>
        /// Gets or sets the subcriber email
        /// </summary>
        [TokenField]
        public string Email { get; set; }

        [TokenField]
        public string Mobile { get; set; }

        [TokenField]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether subscription is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the store identifier in which a customer has subscribed to newsletter
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Nội dung cần tư vấn
        /// </summary>
        [TokenField]
        public string Content { get; set; }

        /// <summary>
        /// Tiêu đề
        /// </summary>
        [TokenField]
        public string Subject { get; set; }

        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when subscription was created
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
        #endregion
    }
}
