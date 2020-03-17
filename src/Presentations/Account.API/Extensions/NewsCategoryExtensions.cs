using System.Collections.Generic;
using System.Linq;
using Vnit.ApplicationCore.Entities.News;
using Vnit.Api.ViewModels.News;
using Vnit.ApplicationCore.Constants;
using Vnit.Services.SEO;

namespace Vnit.Api.Extensions
{
    public static class NewsCategoryExtensions
    {
        public static NewsCategoryModel ToModel(this NewsCategory n)
        {
            if (n == null)
            {
                return null;
            }

            return new NewsCategoryModel()
            {
                Id = n.Id,
                Name = n.Name,
                Short = n.Short,
                ParentId = n.ParentId,
                DisplayOrder = n.DisplayOrder,
                SeName = string.Format("/{0}/{1}", RouteConstants.NewsCategory, n.GetSeName())
            };
        }
        public static NewsCategory ToEntity(this NewsCategoryModel n)
        {
            return new NewsCategory()
            {
                Id = n.Id,
                Name = n.Name,
                Short = n.Short,
                ParentId = n.ParentId,
                DisplayOrder = n.DisplayOrder
            };
        }

        public static List<NewsCategoryModel> ToParentModels(this List<NewsCategory> newsCategories)
        {
            var newsCategoryModels = newsCategories.Select(x => x.ToModel()).ToList();

            List<NewsCategoryModel> parentModels = new List<NewsCategoryModel>();


            foreach (var newsCategoryModel in newsCategoryModels)
            {
                var menuChildrents = newsCategoryModels.FindAll(x => newsCategoryModel.Id == x.ParentId).ToList();
                if (menuChildrents.Any())
                {
                    foreach (var menuChildrent in menuChildrents)
                    {
                        var menuSubChildrents = newsCategoryModels.FindAll(x => menuChildrent.Id == x.ParentId).ToList();
                        if (menuSubChildrents.Any())
                        {
                            menuChildrent.NewsCategoryChildrents = menuSubChildrents;
                            menuChildrent.HasChildrent = true;
                        }
                    }

                    newsCategoryModel.NewsCategoryChildrents = menuChildrents;
                    newsCategoryModel.HasChildrent = true;

                }

                if (!newsCategoryModel.ParentId.HasValue)
                {
                    newsCategoryModel.HasParent = true;
                    parentModels.Add(newsCategoryModel);
                }
            }
            return parentModels;
        }
    }
}