using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Entities.Localization;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.Localization;
using Vnit.Api.ViewModels;

namespace Vnit.Api.Controllers.Api
{
    public class LocaleStringResourceController : BaseApiController
    {
        private readonly ILocaleStringResourceService _localeStringResourceService;

        public LocaleStringResourceController(ILocaleStringResourceService localeStringResourceService)
        {
            _localeStringResourceService = localeStringResourceService;
        }

       
        [HttpGet]
        [Route("get")]
        public IActionResult Get(RootRequestModel requestModel)
        {
            #region predicate
            Expression<Func<LocaleStringResource, bool>> where = x => true;
           
            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd<LocaleStringResource>(where, a => a.ResourceName.Contains(requestModel.Name));

            if (!string.IsNullOrWhiteSpace(requestModel.Content))
                where = ExpressionHelpers.CombineAnd<LocaleStringResource>(where, a => a.ResourceValue.Contains(requestModel.Content));

            #endregion

            var allLocaleStringResource = _localeStringResourceService.GetPagedList(
                where,
                x => x.ResourceName,
                true,
                requestModel.Page - 1,
                requestModel.Count);
            if (allLocaleStringResource == null)
                return RespondFailure();
            var model = allLocaleStringResource;

            return RespondSuccess(model, model.TotalCount);
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public IActionResult GetById(int id)
        {
            var localeStringResource = _localeStringResourceService.Get(id);
            if (localeStringResource == null)
                return RespondFailure();
            var model = localeStringResource;//.ToModel();

            return RespondSuccess(model);
        }

        [HttpPost]
        public IActionResult Post(LocaleStringResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

           
            //save it and respond
            _localeStringResourceService.Insert(model);

            VerboseReporter.ReportSuccess("Thêm ngôn ngữ thành công", "post");
            return RespondSuccess(model);
        }

        [HttpPut]
        public IActionResult Put(LocaleStringResource entityModel)
        {
            var localeStringResource = _localeStringResourceService.FirstOrDefault(x => x.Id == entityModel.Id);
            //save it and respond

            localeStringResource.ResourceName = entityModel.ResourceName;
            localeStringResource.ResourceValue = entityModel.ResourceValue;
            localeStringResource.LanguageId = entityModel.LanguageId;
           
            _localeStringResourceService.Update(localeStringResource);

            VerboseReporter.ReportSuccess("Sửa ngôn ngữ thành công", "post");

            return RespondSuccess(localeStringResource);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var localeStringResource = _localeStringResourceService.FirstOrDefault(x => x.Id == id);
            _localeStringResourceService.Delete(localeStringResource);

            VerboseReporter.ReportSuccess("Xóa ngôn ngữ thành công", "delete");
            return RespondSuccess();
        }
    }
}