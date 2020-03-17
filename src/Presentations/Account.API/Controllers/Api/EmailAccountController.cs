using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Entities.Emails;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.Emails;
using Vnit.ApplicationCore.Services.Security;
using Vnit.Api.ViewModels;

namespace Vnit.Api.Controllers.Api
{
    public class EmailAccountController : BaseApiController
    {
        private readonly IEmailAccountService _emailAccountService;
        private readonly IEmailService _emailSender;
        private readonly ICryptographyService _cryptographyService;


        public EmailAccountController(IEmailAccountService emailAccountService,
            IEmailService emailSender, ICryptographyService cryptographyService)
        {
            _emailAccountService = emailAccountService;
            _emailSender = emailSender;
            _cryptographyService = cryptographyService;
        }


        [HttpGet]
        [Route("get")]
        public IActionResult Get(RootRequestModel requestModel)
        {
            #region predicate
            Expression<Func<EmailAccount, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd(where, a => a.Username.Contains(requestModel.Name));


            #endregion

            var emailAccounts = _emailAccountService.GetPagedList(
                where,
                null,
                false,
                requestModel.Page - 1,
                requestModel.Count);
            if (emailAccounts == null)
                return RespondFailure();

            var model = emailAccounts;//.Select(x => x.ToModel());

            return RespondSuccess(model, emailAccounts.TotalCount);
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public IActionResult GetById(int id)
        {
            var emailAccount = _emailAccountService.Get(id);
            if (emailAccount == null)
                return RespondFailure();
            var model = emailAccount;//.ToModel();
            //model.Password = _cryptographyService.Encrypt(model.Password);

            return RespondSuccess(model);
        }

        [HttpPost]
        public IActionResult Post(EmailAccount model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var emailAccount = model;//.ToEntity();
            // TODO encrypt password
            // emailAccount.Password = _cryptographyService.Encrypt(model.Password);

            //save it and respond
            _emailAccountService.Insert(emailAccount);

            VerboseReporter.ReportSuccess("Thêm EmailMessage thành công", "post");
            return RespondSuccess(emailAccount);
        }

        [HttpPut]
        public IActionResult Put(EmailAccount entityModel)
        {
            var emailAccount = _emailAccountService.FirstOrDefault(x => x.Id == entityModel.Id);
            //save it and respond
            emailAccount.DisplayName = entityModel.DisplayName;
            emailAccount.Host = entityModel.Host;

            emailAccount.Port = entityModel.Port;
            emailAccount.Email = entityModel.Email;
            emailAccount.Username = entityModel.Username;
            if (entityModel.Password.IsNotNullOrEmpty())
            {
                // TODO encrypt password
                emailAccount.Password = entityModel.Password;
                // emailAccount.Password = _cryptographyService.Encrypt(entityModel.Password);
            }

            emailAccount.IsDefault = entityModel.IsDefault;


            _emailAccountService.Update(emailAccount);

            VerboseReporter.ReportSuccess("Sửa EmailAccount thành công", "post");

            return RespondSuccess(emailAccount);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var emailAccount = _emailAccountService.FirstOrDefault(x => x.Id == id);
            _emailAccountService.Delete(emailAccount);

            VerboseReporter.ReportSuccess("Xóa EmailAccount thành công", "delete");
            return RespondSuccess();
        }



        //[HttpPost]
        //public IActionResult SendTestEmail(EmailAccountModel model)
        //{
        //    //if (!_permissionService.Authorize(StandardPermissionProvider.ManageEmailAccounts))
        //    //    return AccessDeniedView();

        //    var emailAccount = _emailAccountService.Get(model.Id);
        //    if (emailAccount == null)
        //        //No email account found with the specified id
        //        return RespondFailure();

        //    if (!(model.SendTestEmailTo).IsValidEmail())
        //    {
        //        //ErrorNotification(("Admin.Common.WrongEmail"), false);
        //        return RespondFailure(model);
        //    }

        //    try
        //    {
        //        if (String.IsNullOrWhiteSpace(model.SendTestEmailTo))
        //            throw new Exception("Enter test email address");

        //        _emailSender.SendTestEmail(model.SendTestEmailTo, emailAccount);

        //        //SuccessNotification("SendTestEmailSuccess", false);
        //    }
        //    catch (Exception exc)
        //    {
        //        //ErrorNotification(exc.Message, false);
        //    }

        //    //If we got this far, something failed, redisplay form
        //    return RespondSuccess(model);
        //}
    }
}