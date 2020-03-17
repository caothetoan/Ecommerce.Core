namespace Vnit.Api.ViewModels.News
{
    
    public partial class NewsItemRequestModel : RootRequestModel
    {
        public string SeName { get; set; }

        public bool? HotNews { get; set; }

        public bool? IsRelated { get; set; }

    }
}