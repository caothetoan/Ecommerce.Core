
namespace Vnit.WebFramework.Models.News
{
    public class NewsCategoryRequestModel: RootRequestModel
    {
        public bool? IsGetParent { get; set; }

        public int CategoryCount { get; set; }

        public string SeName { get; set; }

        public bool? HotNews { get; set; }

        public bool? IsRelated { get; set; }

    }
}