using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vnit.Api.Extensions;
using Vnit.Api.ViewModels;
using Vnit.Api.ViewModels.Widgets;
using Vnit.ApplicationCore.Entities.Widgets;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.SEO;
using Vnit.ApplicationCore.Services.Widgets;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vnit.Api.Controllers.Api
{
   
    public class WidgetController : BaseApiController
    {
        private readonly IWidgetService _WidgetService;

        public WidgetController(IWidgetService widgetService) => _WidgetService = widgetService;

        /// <summary>
        /// Lấy danh sách trang
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get([FromQuery] RootRequestModel requestModel)
        {
            if (requestModel == null)
                return BadRequest();

            if (requestModel.Page < 1)
                requestModel.Page = 1;
            if (requestModel.Count < 1)
                requestModel.Count = 10;

            Expression<Func<Widget, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd<Widget>(where, a => a.Name.Contains(requestModel.Name));

            var Widgets = await _WidgetService.GetPagedListAsync(
                where,
                x => x.DisplayOrder,
                true,
                requestModel.Page - 1,
                requestModel.Count);
            if (Widgets == null)
                return RespondFailure();
            var model = Widgets.Select(x => x.ToModel());

            return RespondSuccess(model, Widgets.TotalCount);
        }

        /// <summary>
        /// Lây chi tiết trang theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var Widget = await _WidgetService.FirstOrDefaultAsync(x => x.Id == id);
            return RespondSuccess(Widget.ToModel());
        }

        /// <summary>
        /// Lấy chi tiết trang theo đường dẫn thân thiện
        /// </summary>
        /// <param name="seName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{seName}")]
        public async Task<IActionResult> Get(string seName)
        {
            var Widget = await _WidgetService.GetBySeNameAsync(seName);
            if (Widget == null)
            {
                VerboseReporter.ReportError("Không tìm thấy trang");
                return RespondFailure();
            }
            var model = Widget.ToModel();

            return RespondSuccess(model);
        }
        [Route("post")]
        [HttpPost]
        public IActionResult Post(WidgetModel model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            var Widget = model.ToEntity();

            Widget.CreatedOnUtc = DateTime.Now;
           
            //save it
            _WidgetService.Insert(Widget);


            VerboseReporter.ReportSuccess("Tạo Widget thành công", "post");
            return RespondSuccess(Widget.ToModel());
        }

        [Route("put")]
        [HttpPut]
        public IActionResult Put(WidgetModel model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            //get  
            var widget = model.ToEntity();

            #region mapping
          
            #endregion
            //save it
            _WidgetService.Update(widget);

            VerboseReporter.ReportSuccess("Sửa Widget thành công", "put");
            return RespondSuccess(widget.ToModel());
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id < 0)
                return RespondFailure();
            _WidgetService.Delete(x => x.Id == id);
            VerboseReporter.ReportSuccess("Xóa Widget thành công", "delete");
            return RespondSuccess();
        }

        [Route("updateStatus/{id:int}")]
        [HttpPut]
        public IActionResult UpdateStatus(int id)
        {
            if (id < 0)
                return RespondFailure();
            var Widget = _WidgetService.FirstOrDefault(x => x.Id == id);
            if (Widget == null)
                return RespondFailure();

            Widget.Published = !Widget.Published;
            _WidgetService.Update(Widget);

            VerboseReporter.ReportSuccess("Cập nhật trạng thái thành công", "updateStatus");
            return RespondSuccess(Widget.ToModel());
        }
    }
}
