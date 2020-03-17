using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Entities.Emails;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.Emails;
using Vnit.Api.ViewModels;

namespace Vnit.Api.Controllers.Api
{
    public class NewsLetterSubscriptionController : BaseApiController
    {
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        //private readonly IExportManager _exportManager;


        public NewsLetterSubscriptionController(INewsLetterSubscriptionService newsLetterSubscriptionService
            //, IExportManager exportManager
            )
        {
            _newsLetterSubscriptionService = newsLetterSubscriptionService;
            //_exportManager = exportManager;
        }

       
        /// <summary>
        /// Lấy ds
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IActionResult Get([FromQuery] RootRequestModel requestModel)
        {
            #region predicate
            Expression<Func<NewsLetterSubscription, bool>> where = x => true;
            DateTime dateFrom, dateTo;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = a => a.Name.Contains(requestModel.Name);

            if (!string.IsNullOrWhiteSpace(requestModel.Email))
                where = ExpressionHelpers.CombineAnd<NewsLetterSubscription>(where, a => a.Email.Contains(requestModel.Email));

            if (!string.IsNullOrWhiteSpace(requestModel.Mobile))
                where = ExpressionHelpers.CombineAnd<NewsLetterSubscription>(where, a => a.Mobile.Contains(requestModel.Mobile));

            if ((requestModel.DateFrom.IsNotNullOrEmpty()))
            {
                dateFrom = requestModel.DateFrom.ToDateTime();
                where = ExpressionHelpers.CombineAnd<NewsLetterSubscription>(where, a => a.CreatedOnUtc >= dateFrom);
            }
            if ((requestModel.DateTo.IsNotNullOrEmpty()))
            {
                dateTo = requestModel.DateTo.ToDateTimeAddOneDay();
                where = ExpressionHelpers.CombineAnd<NewsLetterSubscription>(where, a => a.CreatedOnUtc <= dateTo);
            }
            #endregion

            var newsLetterSubscriptions = _newsLetterSubscriptionService.GetPagedList(
                where,
                null,
                false,
                requestModel.Page - 1,
                requestModel.Count);
            return RespondSuccess(newsLetterSubscriptions, newsLetterSubscriptions.TotalCount);
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public IActionResult GetById(int id)
        {
            return RespondSuccess(_newsLetterSubscriptionService.FirstOrDefault(x => x.Id == id));
        }

        [Route("post")]
        [HttpPost]
        public IActionResult Post(NewsLetterSubscription entityModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var newsLetterSubscription = entityModel;
            newsLetterSubscription.NewsLetterSubscriptionGuid = Guid.NewGuid();
            newsLetterSubscription.CreatedOnUtc = DateTime.Now;
            //save it
            _newsLetterSubscriptionService.Insert(newsLetterSubscription);


            VerboseReporter.ReportSuccess("Tạo thư đăng ký thành công", "post");
            return RespondSuccess(entityModel
            );
        }

        [Route("put")]
        [HttpPut]
        public IActionResult Put(NewsLetterSubscription entityModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            //get  
            var newsLetterSubscription = _newsLetterSubscriptionService.Get(entityModel.Id);
            if (newsLetterSubscription == null)
                return RespondFailure();

            newsLetterSubscription = entityModel;
            newsLetterSubscription.Email = entityModel.Email;
            newsLetterSubscription.Mobile = entityModel.Mobile;
            newsLetterSubscription.Name = entityModel.Name;
            newsLetterSubscription.Active = entityModel.Active;

            newsLetterSubscription.StatusId = entityModel.StatusId;
            //save it
            _newsLetterSubscriptionService.Update(newsLetterSubscription);

            VerboseReporter.ReportSuccess("Sửa thư đăng ký thành công", "put");
            return RespondSuccess(newsLetterSubscription);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _newsLetterSubscriptionService.Delete(x => x.Id == id);
            VerboseReporter.ReportSuccess("Xóa thư đăng ký thành công", "delete");
            return RespondSuccess();
        }

        [Route("updateStatus/{id:int}")]
        [HttpPut]
        public IActionResult UpdateStatus(int id)
        {
            if (id < 0)
                return RespondFailure();
            var newsLetterSubscription = _newsLetterSubscriptionService.FirstOrDefault(x => x.Id == id);
            if (newsLetterSubscription == null)
                return RespondFailure();

            newsLetterSubscription.Active = !newsLetterSubscription.Active;
            _newsLetterSubscriptionService.Update(newsLetterSubscription);

            VerboseReporter.ReportSuccess("Cập nhật trạng thái đã tư vấn thành công", "updateStatus");
            return RespondSuccess();
        }

        ///// <summary>
        ///// Xuất danh sách ra file excel
        ///// </summary>      
        ///// <returns></returns>
        //[HttpPost]
        ////[FormValueRequired("exportexcel-all")]
        //public virtual ActionResult ExportExcel()
        //{
        //    if (!IsAuthorize())
        //    {
        //        VerboseReporter.ReportError("Không có quyền xuất danh sách tư vấn", "import");
        //        return AccessDeniedView();
        //    }
        //    var newsLetterSubscriptions = _newsLetterSubscriptionService.Get(null, null, false).ToList();

        //    try
        //    {
        //        var bytes = _exportManager.ExportNewsLetterSubscriptionToXlsx(newsLetterSubscriptions);

        //        SuccessNotification(("Xuất danh sách tư vấn thành công"));
        //        return File(bytes, MimeTypes.TextXlsx, string.Format("danh_sach_tu_van_{0}.xlsx", DateTime.Now));//.ToDateTimeYYYYMMDDString()
        //    }
        //    catch (Exception exc)
        //    {
        //        ErrorNotification(("Download File thất bại"));
        //        ErrorNotification(exc);
        //        return RedirectToAction("Index");
        //    }
        //}

        //private bool IsAuthorize()
        //{
        //    if (!CurrentUser.IsAdministrator() )
        //    {
        //        return false;
        //    }

        //    return true;
        //}
    }
}