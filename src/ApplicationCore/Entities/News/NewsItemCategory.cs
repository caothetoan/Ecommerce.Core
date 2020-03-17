namespace Vnit.ApplicationCore.Entities.News
{
  
    public class NewsItemCategory : BaseEntity
    {
        public int NewsItemId { get; set; }
        public int NewsCategoryId { get; set; }
        public int DisplayOrder { get; set; }


        public virtual NewsItem NewsItem { get; set; }

        public virtual NewsCategory NewsCategory { get; set; }
    }
}
