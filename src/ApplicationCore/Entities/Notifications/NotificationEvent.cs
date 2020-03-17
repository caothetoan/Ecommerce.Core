using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Entities.Notifications
{
    public class NotificationEvent : BaseEntity
    {
        public string EventName { get; set; }

        public bool Enabled { get; set; }

        public virtual IList<Notification> Notifications { get; set; }
    }
}
