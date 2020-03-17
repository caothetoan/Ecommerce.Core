using Microsoft.AspNetCore.Mvc;
using Vnit.Services.Courses;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Catalog.API.ModelExtensions;
using Catalog.API.Models.Courses;
using Vnit.ApplicationCore.Helpers;
using Vnit.WebFramework.ModelExtensions;
using Vnit.WebFramework.Models;
using Vnit.WebFramework.Models.Catalogs;
using Vnit.ApplicationCore.Entities.Courses;
using Vnit.Services.SEO;

namespace Catalog.API.Controllers
{
    public class CourseController : BaseApiController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService productService) => _courseService = productService;

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

            Expression<Func<Course, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd(where, a => a.Name.Contains(requestModel.Name));

            var products = await _courseService.GetPagedListAsync(
                where,
                x => x.DisplayOrder,
                true,
                requestModel.Page - 1,
                requestModel.Count);
            if (products == null)
                return RespondFailure();
            var model = products.Select(x => x.ToModel());

            return RespondSuccess(model, products.TotalCount);
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
            var product = await _courseService.FirstOrDefaultAsync(x => x.Id == id);
            return RespondSuccess(product);
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
            var product = await _courseService.GetBySeNameAsync(seName);
            if (product == null)
            {
                VerboseReporter.ReportError("Không tìm thấy trang");
                return RespondFailure();
            }
            var model = product.ToModel();

            return RespondSuccess(model);
        }
        [Route("post")]
        [HttpPost]
        public IActionResult Post(CourseModel model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            var entity = model.ToEntity();

            entity.CreatedDate = DateTime.Now;
            entity.Description = model.Description.SanitizeHtml();
            entity.Body = model.Body.SanitizeHtml();
            //save it
            _courseService.Insert(entity);


            VerboseReporter.ReportSuccess("Tạo trang thành công", "post");
            return RespondSuccess(model);
        }

        [Route("put")]
        [HttpPut]
        public IActionResult Put(CourseModel model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            //get  
            var product = _courseService.Get(model.Id);
            if (product == null)
                return RespondFailure();

            #region mapping
            product.Name = model.Name;
            product.Description = model.Description.SanitizeHtml();
            product.Body = model.Body.SanitizeHtml();
            product.DisplayOrder = model.DisplayOrder;
            product.Published = model.Published;
            #endregion
            //save it
            _courseService.Update(product);

            VerboseReporter.ReportSuccess("Sửa trang thành công", "put");
            return RespondSuccess(product.ToModel());
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _courseService.Delete(x => x.Id == id);
            VerboseReporter.ReportSuccess("Xóa trang thành công", "delete");
            return RespondSuccess();
        }

        [Route("updateStatus/{id:int}")]
        [HttpPut]
        public IActionResult UpdateStatus(int id)
        {
            if (id < 0)
                return RespondFailure();
            var product = _courseService.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return RespondFailure();

            product.Published = !product.Published;
            _courseService.Update(product);

            VerboseReporter.ReportSuccess("Cập nhật trạng thái thành công", "updateStatus");
            return RespondSuccess(product.ToModel());
        }
    }
}
