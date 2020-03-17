using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Catalog.API.ModelExtensions;
using Catalog.API.Models.Courses;
using Vnit.ApplicationCore.Entities.Courses;
using Vnit.ApplicationCore.Helpers;
using Vnit.WebFramework.ModelExtensions;
using Vnit.WebFramework.Models;
using Vnit.Services.Courses;
using Vnit.Services.SEO;

namespace Catalog.API.Controllers
{
    public class EnrollmentController : BaseApiController
    {
        private readonly IEnrollmentService _EnrollmentService;

        public EnrollmentController(IEnrollmentService productService) => _EnrollmentService = productService;

        /// <summary>
        /// Lấy danh sách enrollment
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

            Expression<Func<Enrollment, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd(where, a => a.User.FullName.Contains(requestModel.Name));

            var products = await _EnrollmentService.GetPagedListAsync(
                where,
                x => x.CourseId,
                true,
                requestModel.Page - 1,
                requestModel.Count);
            if (products == null)
                return RespondFailure();
            var model = products.Select(x => x.ToModel());

            return RespondSuccess(model, products.TotalCount);
        }

        /// <summary>
        /// Lây chi tiết enrollment theo Id
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
            var product = await _EnrollmentService.FirstOrDefaultAsync(x => x.Id == id);
            return RespondSuccess(product);
        }

        /// <summary>
        /// Lấy chi tiết enrollment theo đường dẫn thân thiện
        /// </summary>
        /// <param name="seName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{seName}")]
        public async Task<IActionResult> Get(string seName)
        {
            var product = await _EnrollmentService.GetBySeNameAsync(seName);
            if (product == null)
            {
                VerboseReporter.ReportError("Không tìm thấy enrollment");
                return RespondFailure();
            }
            var model = product.ToModel();

            return RespondSuccess(model);
        }
        [Route("post")]
        [HttpPost]
        public IActionResult Post(EnrollmentModel model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            var entity = model.ToEntity();

            //save it
            _EnrollmentService.Insert(entity);


            VerboseReporter.ReportSuccess("Tạo enrollment thành công", "post");
            return RespondSuccess(model);
        }

        [Route("put")]
        [HttpPut]
        public IActionResult Put(EnrollmentModel model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            //get  
            var product = _EnrollmentService.Get(model.Id);
            if (product == null)
                return RespondFailure();

            #region mapping
           
            #endregion
            //save it
            _EnrollmentService.Update(product);

            VerboseReporter.ReportSuccess("Sửa enrollment thành công", "put");
            return RespondSuccess(product.ToModel());
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _EnrollmentService.Delete(x => x.Id == id);
            VerboseReporter.ReportSuccess("Xóa enrollment thành công", "delete");
            return RespondSuccess();
        }

    }
}
