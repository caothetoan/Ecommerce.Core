using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Helpers;
using Vnit.WebFramework.ModelExtensions;
using Vnit.WebFramework.Models;
using Vnit.WebFramework.Models.Catalogs;
using Vnit.ApplicationCore.Entities.Catalog;
using Vnit.Services.Catalogs;
using Vnit.Services.SEO;

namespace Catalog.API.Controllers
{
  
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) => _productService = productService;

        /// <summary>
        /// Lấy danh sách sp
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

            Expression<Func<Product, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd<Product>(where, a => a.Name.Contains(requestModel.Name));

            var products = await _productService.GetPagedListAsync(
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
        /// Lây chi tiết sp theo Id
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
            var product = await _productService.FirstOrDefaultAsync(x => x.Id == id);
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
            var product = await _productService.GetBySeNameAsync(seName);
            if (product == null)
            {
                VerboseReporter.ReportError("Không tìm thấy trang");
                return RespondFailure();
            }
            var model = product.ToModel();

            return RespondSuccess(model);
        }
        [HttpPost]
        public IActionResult Post(ProductModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var entity = model.ToEntity();
           
            //save it
            _productService.Insert(entity);


            VerboseReporter.ReportSuccess("Tạo trang thành công", "post");
            return RespondSuccess(model);
        }

        [HttpPut]
        public IActionResult Put(ProductModel model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            //get  
            var product = _productService.Get(model.Id);
            if (product == null)
                return RespondFailure();

            #region mapping
            product.Name = model.Name;
            product.Short = model.Short.SanitizeHtml();
            product.Full = model.Full.SanitizeHtml();
            product.DisplayOrder = model.DisplayOrder;
          
            #endregion
            //save it
            _productService.Update(product);

            VerboseReporter.ReportSuccess("Sửa trang thành công", "put");
            return RespondSuccess(product.ToModel());
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _productService.Delete(x => x.Id == id);
            VerboseReporter.ReportSuccess("Xóa trang thành công", "delete");
            return RespondSuccess();
        }

        [Route("updateStatus/{id:int}")]
        [HttpPut]
        public IActionResult UpdateStatus(int id)
        {
            if (id < 0)
                return RespondFailure();
            var product = _productService.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return RespondFailure();

            product.Published = !product.Published;
            _productService.Update(product);

            VerboseReporter.ReportSuccess("Cập nhật trạng thái thành công", "updateStatus");
            return RespondSuccess(product.ToModel());
        }
    }
}