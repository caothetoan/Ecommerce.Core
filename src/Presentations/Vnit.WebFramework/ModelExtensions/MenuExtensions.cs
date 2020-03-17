using System;
using System.Collections.Generic;
using System.Linq;
using Vnit.ApplicationCore.Entities.Menus;
using Vnit.ApplicationCore.Services.Localization;
using Vnit.WebFramework.Models.Menus;

namespace Vnit.WebFramework.ModelExtensions
{
    public static class MenuExtensions
    {
        public static MenuModel ToModel(this Menu menu)
        {
            if (menu == null) return null;

            MenuModel model = new MenuModel()
            {
                Id = menu.Id,
                Name = menu.Name,
                Url = menu.Url,
                Sequence = menu.Sequence,
                ParentId = menu.ParentId,
                PositionId = menu.PositionId,

                NewWindow = menu.NewWindow,
                CreatedDate = menu.CreatedDate,
                CreatedBy = menu.CreatedBy,
                ModifyDate = menu.ModifyDate,
                ModifyBy = menu.ModifyBy,
                Icon = menu.Icon,
                Active = menu.Active
            };

            return model;
        }
        public static Menu ToEntity(this MenuModel u)
        {
            Menu menu = new Menu()
            {
                Name = u.Name,
                Url = u.Url,
                Sequence = u.Sequence,
                ParentId = u.ParentId,
                PositionId = u.PositionId,
                CreatedDate = DateTime.Now,
                NewWindow = u.NewWindow,
                Icon = u.Icon,
                Active = u.Active
            };

            return menu;
        }


        public static List<MenuModel> ToParentModels(this List<Menu> menus)
        {
            if (menus == null)
                return null;
            var menuModels = menus.ToList().Select(x => x.ToModel()).ToList();

            List<MenuModel> menuModelParents = new List<MenuModel>();


            foreach (var menuModel in menuModels)
            {
                var menuChildrents = menuModels.FindAll(x => menuModel.Id == x.ParentId).ToList();
                if (menuChildrents.Any())
                {
                    foreach (var menuChildrent in menuChildrents)
                    {
                        var menuSubChildrents = menuModels.FindAll(x => menuChildrent.Id == x.ParentId).ToList();
                        if (menuSubChildrents.Any())
                        {
                            menuChildrent.MenuChildrents = menuSubChildrents;
                            menuChildrent.HasChildrent = true;
                        }
                    }

                    menuModel.MenuChildrents = menuChildrents;
                    menuModel.HasChildrent = true;

                }

                if (!menuModel.ParentId.HasValue)
                {
                    menuModel.HasParent = true;
                    menuModelParents.Add(menuModel);
                }
            }
            return menuModelParents;
        }
        public static List<MenuModel> ToParentModels(this List<Menu> menus, int languageId)
        {
            if (menus == null)
                return null;
            var menuModels = menus.ToList().Select(x => x.ToModel(languageId)).ToList();

            List<MenuModel> menuModelParents = new List<MenuModel>();


            foreach (var menuModel in menuModels)
            {
                // menu con cấp 1
                var menuChildrents = menuModels.FindAll(x => menuModel.Id == x.ParentId).OrderBy(n => n.Sequence).ToList();
                if (menuChildrents.Any())
                {
                    foreach (var menuChildrent in menuChildrents)
                    {
                        // menu con cấp 2
                        var menuSubChildrents = menuModels.FindAll(x => x.ParentId == menuChildrent.Id).OrderBy(n => n.Sequence).ToList();
                        if (menuSubChildrents.Any())
                        {
                            menuChildrent.MenuChildrents = menuSubChildrents;
                            menuChildrent.HasChildrent = true;
                        }
                    }

                    menuModel.MenuChildrents = menuChildrents;
                    menuModel.HasChildrent = true;

                }

                if (!menuModel.ParentId.HasValue)
                {
                    menuModel.HasParent = true;
                    menuModelParents.Add(menuModel);
                }
            }
            return menuModelParents;
        }
        public static MenuModel ToModel(this Menu menu, int languageId)
        {
            var model = menu.ToModel();

            if (languageId <= 0)
            {
                return model;
            }
            var localizedProperties = menu.GetLocalizedPropertys(languageId);
            var localePropertyName = localizedProperties.FirstOrDefault(x => x.LocaleKey == "Name");

            if (localePropertyName != null)
                model.Name = localePropertyName?.LocaleValue;
            return model;
        }

        public static void ToModel(this Menu menu, MenuModel entityModel)
        {
            if (entityModel.LanguageId <= 0)
            {
                return;
            }
            var localizedProperties = menu.GetLocalizedPropertys(entityModel.LanguageId);
            var localePropertyName = localizedProperties.FirstOrDefault(x => x.LocaleKey == "Name");
            if (localePropertyName != null)
                entityModel.Name = localePropertyName?.LocaleValue;
        }
    }
}