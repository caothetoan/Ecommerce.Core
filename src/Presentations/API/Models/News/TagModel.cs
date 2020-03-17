
using Vnit.WebFramework.Models;

namespace Vnit.WebFramework.Models.News
{
    public class TagModel: RootModel
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
