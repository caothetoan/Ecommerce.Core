using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Interfaces
{
    public interface ITracking
    {
        DateTime CreatedDate { get; set; }

        int CreatedBy { get; set; }

        DateTime? ModifyDate { get; set; }

        int? ModifyBy { get; set; }
    }
}
