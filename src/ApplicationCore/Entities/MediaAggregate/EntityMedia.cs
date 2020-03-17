
namespace Vnit.ApplicationCore.Entities.MediaAggregate
{
    public class EntityMedia : BaseEntity
    {
        public int MediaId { get; set; }

        public int EntityId { get; set; }


        public string EntityName { get; set; }

        public int DisplayOrder { get; set; }


        //[ForeignKey("MediaId")]
        public virtual Media Media { get; set; }
        
    }
}
