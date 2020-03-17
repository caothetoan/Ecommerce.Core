using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Entities.Emails
{
    /// <summary>
    /// Represents priority of queued email
    /// </summary>
    public enum QueuedEmailPriority
    {
        /// <summary>
        /// Low
        /// </summary>
        Low = 0,

        /// <summary>
        /// High
        /// </summary>
        High = 5
    }
}
