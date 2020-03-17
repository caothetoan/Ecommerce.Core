namespace Vnit.WebFramework.Models.News
{
  
    public partial class NewsItemLocalizedModel : ILocalizedModel
    {
        public int LanguageId { get; set; }

        //[ResourceDisplayName("Admin.Catalog.NewsArticle.Fields.Name")]
        public string Name { get; set; }

        //[ResourceDisplayName("Admin.Catalog.NewsArticle.Fields.ShortDescription")]
        public string Short { get; set; }

        //[ResourceDisplayName("Admin.Catalog.NewsArticle.Fields.FullDescription")]
        public string Full { get; set; }

        //[ResourceDisplayName("Admin.Catalog.NewsArticle.Fields.MetaKeywords")]
        public string MetaKeywords { get; set; }

        //[ResourceDisplayName("Admin.Catalog.NewsArticle.Fields.MetaDescription")]
        public string MetaDescription { get; set; }

        //[ResourceDisplayName("Admin.Catalog.NewsArticle.Fields.MetaTitle")]
        public string MetaTitle { get; set; }

        //[ResourceDisplayName("Admin.Catalog.NewsArticle.Fields.SeName")]
        public string SeName { get; set; }
    }
}