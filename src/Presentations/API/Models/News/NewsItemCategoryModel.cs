using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnit.WebFramework.Models;

namespace Vnit.WebFramework.Models.News
{
    public class NewsItemCategoryModel:RootModel
    {
        public int NewsItemId { get; set; }
        public int NewsCategoryId { get; set; }
        public int DisplayOrder { get; set; }

        public NewsCategoryModel NewsCategory { get; set; }
    }
}
