﻿using Vnit.ApplicationCore.Entities.News;
using Vnit.WebFramework.Models.News;

namespace Vnit.WebFramework.ModelExtensions
{
    public static class TagExtensions
    {
        public static TagModel ToModel(this Tag tag)
        {
            if (tag == null)
                return null;
            return new TagModel()
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayOrder = tag.DisplayOrder,
                CreateDate = tag.CreateDate
            };
        }

        public static Tag ToEntity(this TagModel model)
        {
            if (model == null)
                return null;
            return new Tag()
            {
                Name = model.Name,
                DisplayOrder = model.DisplayOrder,
                CreateDate = model.CreateDate
            };
        }
    }
}