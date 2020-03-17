using System.Collections.Generic;
using System.Linq;
using Vnit.Api.ViewModels.Widgets;
using Vnit.ApplicationCore.Entities.Widgets;
using Vnit.ApplicationCore.Services.Localization;

namespace Vnit.Api.Extensions
{
    public static class GroupPageExtensions
    {
        public static GroupPageModel ToModel(this GroupPage groupPage )
        {
            if (groupPage == null)
            {
                return null;
            }
            var entityModel = new GroupPageModel
            {
                Name = groupPage.Name,
                OrderNo = groupPage.OrderNo,
                ParentId = groupPage.ParentId,
                Icon = groupPage.Icon ?? "",
                ActionName = groupPage.ActionName,
                ControllerName = groupPage.ControllerName,
                Active = groupPage.Active,
                Description = groupPage.Description,
                Body = groupPage.Body,
                Level = groupPage.Level,
                Id = groupPage.Id,
                UnsignedName = groupPage.UnsignedName,
                CreateDate = groupPage.CreateDate,
                CreateBy = groupPage.CreateBy,
                Url = groupPage.Url,
                ServiceId = groupPage.ServiceId,
                IsSystem = groupPage.IsSystem,
                Version = groupPage.Version,
                Cover = groupPage.Cover,
                Style = groupPage.Style,
                //EntityMedias = groupPage.EntityMedias?.Select(x=>x.ToModel()).ToList()
            };

            return entityModel;
        }
        public static GroupPageModel ToModel(this GroupPage groupPage, int languageId)
        {
            if (groupPage == null)
            {
                return null;
            }
            var model = groupPage.ToModel();

            if (languageId <= 0)
            {
                return model;
            }
            var localizedProperties = groupPage.GetLocalizedPropertys(languageId);
            var localePropertyName = localizedProperties.FirstOrDefault(x => x.LocaleKey == "Name");
            if (localePropertyName != null)
                model.Name = localePropertyName?.LocaleValue;

            var localePropertyDescription = localizedProperties.FirstOrDefault(x => x.LocaleKey == "Description");
            if (localePropertyDescription != null)
                model.Description = localePropertyDescription?.LocaleValue;

            var localePropertyBody = localizedProperties.FirstOrDefault(x => x.LocaleKey == "Body");
            if (localePropertyBody != null)
                model.Body = localePropertyBody?.LocaleValue;

            return model;
        }
        public static void ToModel(this GroupPage groupPage, GroupPageModel entityModel)
        {
            if (entityModel.LanguageId <= 0)
            {
                return;
            }
            var localizedProperties = groupPage.GetLocalizedPropertys(entityModel.LanguageId);
            var localePropertyName = localizedProperties.FirstOrDefault(x => x.LocaleKey == "Name");
            if (localePropertyName != null)
                entityModel.Name = localePropertyName?.LocaleValue;

            var localePropertyDescription = localizedProperties.FirstOrDefault(x => x.LocaleKey == "Description");
            if (localePropertyDescription != null)
                entityModel.Description = localePropertyDescription?.LocaleValue;

            var localePropertyBody = localizedProperties.FirstOrDefault(x => x.LocaleKey == "Body");
            if (localePropertyBody != null)
                entityModel.Body = localePropertyBody?.LocaleValue;
        }


        public static GroupPage ToEntity(this GroupPageModel model)
        {
            var module = new GroupPage
            {
                Name = model.Name,
                OrderNo = model.OrderNo,
                ParentId = model.ParentId,
                Icon = model.Icon ?? "",
                ActionName = model.ActionName,
                ControllerName = model.ControllerName,
                Active = model.Active,
                Description = model.Description,
                Body = model.Body,
                Level = model.Level,
                Id = model.Id,
                UnsignedName = model.UnsignedName,
                Url = model.Url,
                ServiceId = model.ServiceId,
                IsSystem = model.IsSystem,
                Version = model.Version,
                Cover = model.Cover,
                Style = model.Style,
            };

            return module;
        }

        public static List<GroupPageModel> ToParentModels(this List<GroupPage> groupPages, int languageId)
        {
            var groupPageModels = groupPages.Select(x => x.ToModel()).ToList();


            List<GroupPageModel> groupPageParents = new List<GroupPageModel>();
            foreach (var groupPageModel in groupPageModels)
            {
                var groupPageChildrents = groupPageModels.FindAll(x => groupPageModel.Id == x.ParentId).ToList();
                if (groupPageChildrents.Any())
                {
                    foreach (var childrent in groupPageChildrents)
                    {
                        var subChildrents = groupPageModels.FindAll(x => childrent.Id == x.ParentId).OrderBy(x => x.OrderNo).ToList();
                        if (subChildrents.Any())
                        {
                            childrent.GroupPageChildrents = subChildrents;
                            childrent.HasChildrent = true;
                        }
                    }

                    groupPageModel.GroupPageChildrents = groupPageChildrents;
                    groupPageModel.HasChildrent = true;

                }

                if (!groupPageModel.ParentId.HasValue)
                {
                    groupPageModel.HasParent = true;
                    groupPageParents.Add(groupPageModel);
                }
            }
            return groupPageParents;
        }

    }
}
