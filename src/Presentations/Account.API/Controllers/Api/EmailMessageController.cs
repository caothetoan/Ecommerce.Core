using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Entities.Emails;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.Emails;
using Vnit.Api.ViewModels;

namespace Vnit.Api.Controllers.Api
{
    public class EmailMessageController : BaseApiController
    {
         #region variables
        private readonly IEmailMessageService _emailService;

        public EmailMessageController(IEmailMessageService emailService)
        {
            _emailService = emailService;
        }

        #endregion

    

        [HttpGet]
        [Route("get")]
        public IActionResult Get(RootRequestModel requestModel)
        {
            #region predicate
            Expression<Func<EmailMessage, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd(where, a => a.To.Contains(requestModel.Name));


            #endregion

            var allEmailMessage = _emailService.GetPagedList(
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
            var emailMessage = _emailService.Get(id);
            if (emailMessage == null)
                return RespondFailure();
            var model = emailMessage;//.ToModel();

            return RespondSuccess(model);
        }

        [HttpPost]
        public IActionResult Post(EmailMessage model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var emailMessage = model;//.ToEntity();
            //save it and respond
            _emailService.Insert(emailMessage);

            VerboseReporter.ReportSuccess("Thêm EmailMessage thành công", "post");
            return RespondSuccess(emailMessage);
        }

        [HttpPut]
        public IActionResult Put(EmailMessage entityModel)
        {
            var emailMessage = _emailService.FirstOrDefault(x => x.Id == entityModel.Id);
            //save it and respond

          
            //_emailService.Update(emailMessage);

            VerboseReporter.ReportSuccess("Sửa EmailMessage thành công", "post");

            return RespondSuccess(emailMessage);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var emailMessage = _emailService.FirstOrDefault(x => x.Id == id);
            _emailService.Delete(emailMessage);

            VerboseReporter.ReportSuccess("Xóa EmailMessage thành công", "delete");
            return RespondSuccess();
        }

    }   
}