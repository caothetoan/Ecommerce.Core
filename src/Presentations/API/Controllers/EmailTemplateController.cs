using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Entities.Emails;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.Emails;
using Vnit.WebFramework.Models;

namespace Catalog.API.Controllers
{

    public class EmailTemplateController : BaseApiController
    {
        #region variables
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailAccountService _emailAccountService;
        public EmailTemplateController(IEmailTemplateService emailTemplateService, IEmailAccountService emailAccountService)
        {
            _emailTemplateService = emailTemplateService;
            _emailAccountService = emailAccountService;
        }

        #endregion


        [HttpGet]
        [Route("get")]
        public IActionResult Get(RootRequestModel requestModel)
        {
            #region predicate
            Expression<Func<EmailTemplate, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd(where, a => a.Name.Contains(requestModel.Name));


            #endregion

            var allEmailMessage = _emailTemplateService.GetPagedList(
                where,
                null,
                false,
                requestModel.Page - 1,
                requestModel.Count);
            if (allEmailMessage == null)
                return RespondFailure();
            var model = allEmailMessage;

            return RespondSuccess(model, model.TotalCount);
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public IActionResult GetById(int id)
        {
            var emailMessage = _emailTemplateService.Get(id);
            if (emailMessage == null)
                return RespondFailure();
            var model = emailMessage;//.ToModel();

            return RespondSuccess(model);
        }

        [HttpPost]
        public IActionResult Post(EmailTemplate model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var emailMessage = model;//.ToEntity();
            //save it and respond
            _emailTemplateService.Insert(emailMessage);

            VerboseReporter.ReportSuccess("Thêm EmailTemplate thành công", "post");
            return RespondSuccess(emailMessage);
        }

        [HttpPut]
        public IActionResult Put(EmailTemplate entityModel)
        {
            var emailTemplate = _emailTemplateService.FirstOrDefault(x => x.Id == entityModel.Id);
            //save it and respond
            emailTemplate.Subject = entityModel.Subject;
            emailTemplate.Name = entityModel.Name;
            emailTemplate.Template = entityModel.Template;
            emailTemplate.AdministrationEmail = entityModel.AdministrationEmail;
            emailTemplate.IsMaster = entityModel.IsMaster; //a system template can't be used as master
            if (entityModel.EmailAccountId > 0)
                emailTemplate.EmailAccountId = entityModel.EmailAccountId;

            _emailTemplateService.Update(emailTemplate);

            VerboseReporter.ReportSuccess("Sửa EmailTemplate thành công", "post");

            return RespondSuccess(emailTemplate);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var emailTemplate = _emailTemplateService.FirstOrDefault(x => x.Id == id);
            //if (emailTemplate.IsSystem)
            //{
            //    ErrorNotification("Can't delete a system template");
            //    return RespondFailure();
            //}
            _emailTemplateService.Delete(emailTemplate);

            VerboseReporter.ReportSuccess("Xóa EmailMessage thành công", "delete");
            return RespondSuccess();
        }



        //[System.Web.Mvc.HttpPost]
        //public ActionResult Get(EmailTemplateModel entityModel)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    ErrorNotification(RsCommon.ModelStateIsNotValid);
        //    //    return View();
        //    //}

        //    //get the account
        //    var emailTemplate = _emailTemplateService.Get(entityModel.Id);
        //    if (emailTemplate == null)
        //        return RedirectToAction("NotFound", "Error");

        //    
        //    emailTemplate.EmailAccountId = entityModel.EmailAccountId;
        //    
        //    emailTemplate.ParentEmailTemplateId = entityModel.ParentEmailTemplateId;
        //    emailTemplate.Subject = entityModel.Subject;
        //    emailTemplate.Template = entityModel.Template;
        //    emailTemplate.TemplateName = entityModel.TemplateName;

        //    //save it
        //    _emailTemplateService.Update(emailTemplate);

        //    SuccessNotification("Successfully updated email template");
        //    var model = emailTemplate.Map<EmailTemplateModel>();

        //    return View("Get", model);
        //}


    }
}