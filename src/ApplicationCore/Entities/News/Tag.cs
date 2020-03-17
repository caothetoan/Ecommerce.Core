
namespace Vnit.ApplicationCore.Entities.News
{
    public partial class Tag : BaseEntity
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public System.DateTime CreateDate { get; set; }
        
    }
}

