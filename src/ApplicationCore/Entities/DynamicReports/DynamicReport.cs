using System;

namespace Vnit.ApplicationCore.Entities.DynamicReports
{

    public partial class DynamicReport: BaseEntity
    {
       
        public DynamicReport()
        {
            //this.DOI_TAC_TIEU_CHI = new HashSet<DOI_TAC_TIEU_CHI>();
        }

        public string Name { get; set; }

        public int? DynamicReportType { get; set; }

        public string Code { get; set; }

        public bool Published { get; set; }

        public DateTime? UpdatedOnUtc { get; set; }

        public string Script { get; set; }

        public int? ParentId { get; set; }

        public string Level { get; set; }

        public long? GroupId { get; set; }

        //public virtual ICollection<DOI_TAC_TIEU_CHI> DOI_TAC_TIEU_CHI { get; set; }
    }
}
