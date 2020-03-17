namespace Vnit.ApplicationCore.Entities.News
{
  
    public class NewsItemTag : BaseEntity
    {
        public int NewsItemId { get; set; }
        public int TagId { get; set; }
        public int DisplayOrder { get; set; }

        public virtual NewsItem NewsItem { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
