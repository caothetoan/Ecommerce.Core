using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Entities.Localization;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.Localization;
using Vnit.Api.ViewModels;

namespace Vnit.Api.Controllers.Api
{
    public class LocalizedPropertyController : BaseApiController
    {
        private readonly ILocalizedPropertyService _localizedPropertyService;

        public LocalizedPropertyController(ILocalizedPropertyService localizedPropertyService)
        {
            _localizedPropertyService = localizedPropertyService;
        }

      

        [HttpGet]
        [Route("get")]
        public IActionResult Get(RootRequestModel requestModel)
        {
            #region predicate
            Expression<Func<LocalizedProperty, bool>> where = x => true;
           
            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd<LocalizedProperty>(where, a => a.LocaleKey.Contains(requestModel.Name));

            if (!string.IsNullOrWhiteSpace(requestModel.Content))
                where = ExpressionHelpers.CombineAnd<LocalizedProperty>(where, a => a.LocaleValue.Contains(requestModel.Content));

            #endregion

            var allLocalizedProperty = _localizedPropertyService.GetPagedList(
                where,
                x => x.Id,
                false,
                requestModel.Page - 1,
                requestModel.Count);
            if (allLocalizedProperty == null)
                return RespondFailure();
            var model = allLocalizedProperty;

            return RespondSuccess(model, model.TotalCount);
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public IActionResult GetById(int id)
        {
            var localizedProperty = _localizedPropertyService.Get(id);
            if (localizedProperty == null)
                return RespondFailure();
            var model = localizedProperty;//.ToModel();

            return RespondSuccess(model);
        }

        [HttpPost]
        public IActionResult Post(LocalizedProperty model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

           
            //save it and respond
            _localizedPropertyService.Insert(model);

            VerboseReporter.ReportSuccess("Thêm ngôn ngữ thành công", "post");
            return RespondSuccess(model);
        }

        [HttpPut]
        public IActionResult Put(LocalizedProperty entityModel)
        {
            var localizedProperty = _localizedPropertyService.FirstOrDefault(x => x.Id == entityModel.Id);
            //save it and respond

            localizedProperty.LocaleKey = entityModel.LocaleKey;
            localizedProperty.LocaleValue = entityModel.LocaleValue;
            localizedProperty.LanguageId = entityModel.LanguageId;
           
            _localizedPropertyService.Update(localizedProperty);

            VerboseReporter.ReportSuccess("Sửa ngôn ngữ thành công", "post");

            return RespondSuccess(localizedProperty);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var localizedProperty = _localizedPropertyService.FirstOrDefault(x => x.Id == id);
            _localizedPropertyService.Delete(localizedProperty);

            VerboseReporter.ReportSuccess("Xóa ngôn ngữ thành công", "delete");
            return RespondSuccess();
        }
    }
}