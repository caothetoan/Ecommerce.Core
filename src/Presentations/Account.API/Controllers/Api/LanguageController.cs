using System;
using System.Linq.Expressions;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Entities.Localization;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Interfaces;
using Vnit.ApplicationCore.Services.Languages;
using Vnit.Api.ViewModels;

namespace Vnit.Api.Controllers.Api
{

    public class LanguageController : BaseApiController
    {
        private readonly ILanguageService _languageService;
        private readonly IWorkContext _workContext;

        public LanguageController(ILanguageService languageService, IWorkContext workContext)
        {
            _languageService = languageService;
            _workContext = workContext;
        }

        //public virtual ActionResult SetLanguage(int id, string returnUrl = "")
        //{
        //    var language = _languageService.Get(id);
        //    if (language != null)
        //    {
        //        _workContext.WorkingLanguage = language;
        //    }

        //    //home page
        //    if (String.IsNullOrEmpty(returnUrl))
        //        returnUrl = Url.Action("Index", "Home");
        //    //prevent open redirection attack
        //    if (!Url.IsLocalUrl(returnUrl))
        //        return RedirectToAction("Index", "Home");
        //    return Redirect(returnUrl);
        //}
        //[ChildActionOnly]
        //public virtual ActionResult LanguageSelector()
        //{
        //    var model = new LanguageSelectorModel();
        //    model.CurrentLanguage = _workContext.WorkingLanguage.ToModel();
        //    model.AvailableLanguages = _languageService
        //        .GetAllLanguages(storeId: _storeContext.CurrentStore.Id)
        //        .Select(x => x.ToModel())
        //        .ToList();
        //    return PartialView(model);
        //}

        // GET: Language
     
       
        [HttpGet]
        [Route("")]
        public IActionResult Get([FromQuery] RootRequestModel requestModel)
        {
            Expression<Func<Language, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd<Language>(where, a => a.Name.Contains(requestModel.Name));

            var languages = _languageService.GetPagedList(
                where,
                null,
                false,
                requestModel.Page - 1,
                requestModel.Count);
            if (languages == null)
                return RespondFailure();
            var model = languages;

            return RespondSuccess(model, model.TotalCount);
        }

        [Route("get/{id:int}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var role = _languageService.FirstOrDefault(x => x.Id == id);
            return RespondSuccess(role);
        }

        [Route("post")]
        [HttpPost]
        public IActionResult Post(Language model)
        {
            
            //save it
            _languageService.Insert(model);


            VerboseReporter.ReportSuccess("Tạo ngôn ngữ thành công", "post");
            return RespondSuccess(model);
        }

        [Route("put")]
        [HttpPut]
        public IActionResult Put(Language entityModel)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            //get  
            var language = _languageService.Get(entityModel.Id);
            if (language == null)
                return RespondFailure();

            language.Name = entityModel.Name;
            language.Published = entityModel.Published;
            language.LanguageCulture = entityModel.LanguageCulture;

            language.DisplayOrder = entityModel.DisplayOrder;
            language.FlagImageFileName = entityModel.FlagImageFileName;
            language.UniqueSeoCode = entityModel.UniqueSeoCode;
            //save it
            _languageService.Update(language);

            VerboseReporter.ReportSuccess("Sửa ngôn ngữ thành công", "put");
            return RespondSuccess(language);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _languageService.Delete(x => x.Id == id);
            VerboseReporter.ReportSuccess("Xóa ngôn ngữ thành công", "delete");
            return RespondSuccess();
        }

        [Route("updateStatus/{id:int}")]
        [HttpPut]
        public IActionResult UpdateStatus(int id)
        {
            if (id < 0)
                return RespondFailure();
            var language = _languageService.FirstOrDefault(x => x.Id == id);
            if (language == null)
                return RespondFailure();

            language.Published = !language.Published;
            _languageService.Update(language);

            VerboseReporter.ReportSuccess("Cập nhật trạng thái thành công", "updateStatus");
            return RespondSuccess();
        }
    }
}