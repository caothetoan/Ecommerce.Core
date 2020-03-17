using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Entities.Menus;
using Vnit.ApplicationCore.Entities.Security;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.Localization;
using Vnit.ApplicationCore.Services.Menus;
using Vnit.ApplicationCore.Services.PermissionRecords;
using Vnit.Api.Extensions;
using Vnit.Api.ViewModels;
using Vnit.Api.ViewModels.Menus;

namespace Vnit.Api.Controllers.Api
{
    //[SuperAdminAuthorize]
    public class MenuController : BaseApiController
    {
        #region Private
        private readonly IMenuService _menuService;

        private readonly IPermissionRecordService _permissionRecordService;
        private readonly ILocalizedPropertyService _localizedPropertyService;

        #endregion

        #region Ctors
        public MenuController(IMenuService menuService, IPermissionRecordService permissionRecordService, ILocalizedPropertyService localizedPropertyService)
        {
            _menuService = menuService;
            _permissionRecordService = permissionRecordService;
            _localizedPropertyService = localizedPropertyService;
        }
        #endregion

        #region Utilities
        [NonAction]
        protected virtual void SaveLocalizedValue(Menu service, MenuModel entityModel)
        {
            if (entityModel.LanguageId <= 0)
            {
                return;
            }

            _localizedPropertyService.SaveLocalizedValue(service,
                x => x.Name,
                entityModel.Name,
                entityModel.LanguageId);

            VerboseReporter.ReportSuccess("Sửa ngôn ngữ thành công", "put");
        }


        [NonAction]
        protected virtual void GetLocales(Menu service, MenuModel entityModel)
        {
            if (entityModel.LanguageId <= 0)
            {
                return;
            }
            var localizedProperties = service.GetLocalizedPropertys(entityModel.LanguageId);
            var localePropertyName = localizedProperties.FirstOrDefault(x => x.LocaleKey == "Name");
            if (localePropertyName != null)
                entityModel.Name = localePropertyName?.LocaleValue;
        }
        #endregion



        #region Get
        [Route("getAsync")]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var menus = await _menuService.GetAsync();
            var models = menus.ToList().Select(x => x.ToModel());
            return RespondSuccess(models);
        }

        [Route("")]
        [HttpGet]
        public IActionResult Get([FromQuery] MenuRequestModel requestModel)
        {
            Expression<Func<Menu, bool>> where = x => true;
            DateTime dateFrom, dateTo;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd(where, a => a.Name.Contains(requestModel.Name));
            if ((requestModel.DateFrom.IsNotNullOrEmpty()))
            {
                dateFrom = requestModel.DateFrom.ToDateTime();
                where = ExpressionHelpers.CombineAnd(where, a => a.CreatedDate >= dateFrom);
            }
            if ((requestModel.DateTo.IsNotNullOrEmpty()))
            {
                dateTo = requestModel.DateTo.ToDateTime().AddDays(1);
                where = ExpressionHelpers.CombineAnd(where, a => a.CreatedDate <= dateTo);
            }
            var services = _menuService.GetPagedList(where, x => x.Sequence, true, requestModel.Page - 1, requestModel.Count);

            if (services == null)
                return RespondFailure();

            List<MenuModel> serviceModels = services.Select(x => x.ToModel(requestModel.LanguageId)).ToList();

            return RespondSuccess(serviceModels, services.TotalCount);
        }

        /// <summary>
        /// Lấy danh sách menu cha chứa ds menu con 
        /// Check phân quyền
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getParents")]
        public IActionResult GetParents([FromQuery] MenuRequestModel requestModel)
        {
            #region predicate
            Expression<Func<Menu, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd(where, a => a.Name.Contains(requestModel.Name));

            if (requestModel.PositionId.HasValue)
            {
                where = ExpressionHelpers.CombineAnd(where, x => x.PositionId == requestModel.PositionId);
            }
            if (requestModel.ParentId.HasValue)
            {
                where = ExpressionHelpers.CombineAnd(where, x => x.ParentId == requestModel.ParentId);
            }
            if (requestModel.Active.HasValue)
            {
                where = ExpressionHelpers.CombineAnd(where, x => x.Active == requestModel.Active);
            }
            #endregion

            var menus = _menuService.Get(
                where).ToList();

            #region get permissions
            // TODO fix menus null exception
            //foreach (var userRole in CurrentUser.UserRoles)
            //{
            //    var permissions = _permissionRecordService.GetPermissionRecords(userRole.Role);
            //    foreach (var permission in permissions)
            //    {
            //        menus = menus.Where(x => x.Url.Contains(permission.Category)).ToList();

            //    }
            //}

            #endregion
            List<MenuModel> menuModelParents = menus.ToParentModels(requestModel.LanguageId);

            return RespondSuccess(menuModelParents, menuModelParents.Count);
        }

        public IList<Menu> GetMenusByIdList(IList<int> entityIds)
        {
            return _menuService.Get(x => entityIds.Contains(x.ParentId.Value)).ToList();
        }

        [Route("get/{id:int}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var menu = _menuService.FirstOrDefault(x => x.Id == id);
            var model = menu.ToModel();
            GetLocales(menu, model);

            return RespondSuccess(model);
        }

        [HttpGet]
        public IActionResult GetByIdLocale(RootRequestModel requestModel)
        {
            var menu = _menuService.FirstOrDefault(x => x.Id == requestModel.Id);
            var model = menu.ToModel();
            model.LanguageId = requestModel.LanguageId;
            GetLocales(menu, model);

            return RespondSuccess(model);
        }

        [HttpGet]
        public IActionResult GetByPositionId(int id)
        {
            var menu = _menuService.Get(x => x.PositionId == id);

            return RespondSuccess(menu.ToList().ToParentModels());
        }

        //[HttpGet]
        //public IActionResult GetPositions()
        //{
        //    return RespondSuccess(SelectListItemExtension.GetEnums<WebsitePositionEnum>());
        //}
        #endregion

        #region update
        [HttpPost]
        public IActionResult Post(MenuModel entityModel)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            var menu = entityModel.ToEntity();

            menu.CreatedBy = CurrentUser.Id;
            //save it
            _menuService.Insert(menu);

            var model = menu.ToModel();

            SaveLocalizedValue(menu, model);

            VerboseReporter.ReportSuccess("Tạo Menu thành công", "post");
            return RespondSuccess(model);
        }

        [HttpPut]
        public IActionResult Put(MenuModel entityModel)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            //get  
            var menu = _menuService.Get(entityModel.Id);
            if (menu == null)
                return RespondFailure();

            menu.Name = entityModel.Name;
            menu.Sequence = entityModel.Sequence;
            menu.ParentId = entityModel.ParentId;
            menu.Url = entityModel.Url;
            menu.PositionId = entityModel.PositionId;
            menu.Icon = entityModel.Icon;
            menu.Active = entityModel.Active;
            menu.ModifyDate = DateTime.Now;
            menu.ModifyBy = CurrentUser.Id;
            menu.NewWindow = entityModel.NewWindow;
            //save it
            _menuService.Update(menu);

            //var model = menu.ToModel();

            SaveLocalizedValue(menu, entityModel);

            VerboseReporter.ReportSuccess("Sửa Menu thành công", "put");
            return RespondSuccess(entityModel);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _menuService.Delete(x => x.Id == id);
            VerboseReporter.ReportSuccess("Xóa Menu thành công", "delete");
            return RespondSuccess();
        }
        [Route("updateStatus/{id:int}")]
        [HttpPut]
        public IActionResult UpdateStatus(int id)
        {
            if (id < 0)
                return RespondFailure();
            var service = _menuService.FirstOrDefault(x => x.Id == id);
            if (service == null)
                return RespondFailure();

            service.Active = !service.Active;
            _menuService.Update(service);

            VerboseReporter.ReportSuccess("Cập nhật trạng thái thành công", "updateStatus");
            return RespondSuccess(service);
        } 
        #endregion
      
    }

   
}