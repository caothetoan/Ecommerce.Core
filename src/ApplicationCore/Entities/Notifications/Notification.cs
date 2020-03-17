using System;
using System.Collections.Generic;
using System.Text;
using Vnit.ApplicationCore.Entities.Customers;

namespace Vnit.ApplicationCore.Entities.Notifications
{
    public class Notification : BaseEntity
    {
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public bool IsRead { get; set; }

        public DateTime PublishDateTime { get; set; }

        public DateTime? ReadDateTime { get; set; }

        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public int? NotificationEventId { get; set; }

        public virtual NotificationEvent NotificationEvent { get; set; }

        public int InitiatorId { get; set; }

        public string InitiatorName { get; set; }
    }

}
