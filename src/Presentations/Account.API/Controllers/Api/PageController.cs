using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Entities.Pages;
using Vnit.ApplicationCore.Helpers;
using Vnit.Api.Extensions;
using Vnit.Api.ViewModels;
using Vnit.Api.ViewModels.Pages;
using Vnit.ApplicationCore.Services.SEO;
using Vnit.Services.Pages;

namespace Vnit.Api.Controllers.Api
{
  
    [ApiController]
    public class PageController : BaseApiController
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService) => _pageService = pageService;

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

            Expression<Func<Page, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd<Page>(where, a => a.Name.Contains(requestModel.Name));

            var pages = await _pageService.GetPagedListAsync(
                where,
                x => x.DisplayOrder,
                true,
                requestModel.Page - 1,
                requestModel.Count);
            if (pages == null)
                return RespondFailure();
            var model = pages.Select(x => x.ToModel());

            return RespondSuccess(model, pages.TotalCount);
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
            var page = await _pageService.FirstOrDefaultAsync(x => x.Id == id);
            return RespondSuccess(page.ToModel());
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
            var page = await _pageService.GetBySeNameAsync(seName);
            if (page == null)
            {
                VerboseReporter.ReportError("Không tìm thấy trang");
                return RespondFailure();
            }
            var model = page.ToModel();

            return RespondSuccess(model);
        }
        [Route("post")]
        [HttpPost]
        public IActionResult Post(PageModel model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            var page = model.ToEntity();

            page.CreateDate = DateTime.Now;
            page.Description = model.Description.SanitizeHtml();
            page.Body = model.Body.SanitizeHtml();
            //save it
            _pageService.Insert(page);


            VerboseReporter.ReportSuccess("Tạo trang thành công", "post");
            return RespondSuccess(page.ToModel());
        }

        [Route("put")]
        [HttpPut]
        public IActionResult Put(PageModel model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            //get  
            var page = _pageService.Get(model.Id);
            if (page == null)
                return RespondFailure();

            #region mapping
            page.Name = model.Name;
            page.Description = model.Description.SanitizeHtml();
            page.Body = model.Body.SanitizeHtml();
            page.DisplayOrder = model.DisplayOrder;
            page.Active = model.Active;
            page.Icon = model.Icon;
            page.Url = model.Url;
            #endregion
            //save it
            _pageService.Update(page);

            VerboseReporter.ReportSuccess("Sửa trang thành công", "put");
            return RespondSuccess(page.ToModel());
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id < 0)
                return RespondFailure();
            _pageService.Delete(x => x.Id == id);
            VerboseReporter.ReportSuccess("Xóa trang thành công", "delete");
            return RespondSuccess();
        }

        [Route("updateStatus/{id:int}")]
        [HttpPut]
        public IActionResult UpdateStatus(int id)
        {
            if (id < 0)
                return RespondFailure();
            var page = _pageService.FirstOrDefault(x => x.Id == id);
            if (page == null)
                return RespondFailure();

            page.Active = !page.Active;
            _pageService.Update(page);

            VerboseReporter.ReportSuccess("Cập nhật trạng thái thành công", "updateStatus");
            return RespondSuccess(page.ToModel());
        }
    }
}