using System;

namespace Vnit.ApplicationCore.Entities.DynamicReports
{
    public class DynamicReportSetting : BaseEntity
    {
        
        public string Name { get; set; }

        public string Code { get; set; }

        public byte[] Template { get; set; }

        public long? CapHoc { get; set; }

        public bool? Published { get; set; }

        public int? Type { get; set; }

        public DateTime? UpdatedOnUtc { get; set; }

        public int KhoiHoc { get; set; }

        public int HocKy { get; set; }
    }
}
