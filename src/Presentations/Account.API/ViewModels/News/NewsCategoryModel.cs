using System;
using System.Collections.Generic;

namespace Vnit.Api.ViewModels.News
{
    public class NewsCategoryModel : RootModel
    {

        public string Name { get; set; }

        public string Short { get; set; }

        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }

        public int? ParentId { get; set; }

        #region navigation
        public List<NewsItemModel> NewsArticleModels { get; set; }

        public List<NewsCategoryModel> NewsCategoryChildrents { get; set; }

        public bool HasChildrent { get; set; }

        public bool HasParent { get; set; }
        
        public string SeName { get; set; }
        #endregion
    }
}