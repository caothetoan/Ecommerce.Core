﻿using Vnit.WebFramework.Models;

namespace Vnit.WebFramework.Models.News
{
    public class NewsItemTagModel : RootModel
    {
        public int NewsItemId { get; set; }

        public int TagId { get; set; }

        public int DisplayOrder { get; set; }

        public TagModel Tag { get; set; }
    }
  
}