using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Helpers;
using Vnit.Api.Extensions;
using Vnit.Api.ViewModels;
using Vnit.Api.ViewModels.Catalogs;
using Vnit.ApplicationCore.Entities.Catalog;
using Vnit.ApplicationCore.Services.SEO;
using Vnit.Services.Catalogs;

namespace Vnit.Api.Controllers.Api
{
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

       
        /// <summary>
        /// Lấy ds danh mục
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RootRequestModel requestModel)
        {
            #region predicate
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (requestModel == null)
                return BadRequest();

            if (requestModel.Page < 1)
                requestModel.Page = 1;
            if (requestModel.Count < 1)
                requestModel.Count = 10;
            Expression<Func<Category, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = a => a.Name.Contains(requestModel.Name);
                      
            bool ascending = true;

            if (requestModel.Ascending.HasValue)
                ascending = requestModel.Ascending.Value;
            #endregion

            IPagedList<Category> categories;

            switch (requestModel.OrderBy)
            {
                case "Name":
                    categories = await _categoryService.GetPagedListAsync(
                        where,
                        x => x.Name,
                        ascending,
                        requestModel.Page - 1,
                        requestModel.Count);

                    break;
                default:
                    categories = await _categoryService.GetPagedListAsync(
                        where,
                        x => x.DisplayOrder,
                        ascending,
                        requestModel.Page - 1,
                        requestModel.Count);

                    break;
            }

           
            var model = categories.Select(x => x.ToModel()).ToList();


            return RespondSuccess(model, categories.TotalCount);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            #region predicate
            Expression<Func<Category, bool>> where = x => true;

           
            where = ExpressionHelpers.CombineAnd(where, x => x.Id == id); 
            #endregion

            var category = await _categoryService.FirstOrDefaultAsync(where);
            if (category == null)
                return RespondFailure();

            return RespondSuccess(category.ToModel());
        }

        /// <summary>
        /// Lấy chi tiết danh mục theo đường dẫn thân thiện
        /// </summary>
        /// <param name="seName"></param>
        /// <returns></returns>
        [HttpGet("{seName}")]
        public async Task<IActionResult> Get([FromRoute] string seName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var page = await _categoryService.GetBySeNameAsync(seName);
            if (page == null)
            {
                VerboseReporter.ReportError("Không tìm thấy danh mục");
                return RespondFailure();
            }
            var model = page.ToModel();

            return RespondSuccess(model);
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Post([FromBody] CategoryPostModel entityModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = entityModel.ToEntity();
            //save it
            _categoryService.Insert(category);

            VerboseReporter.ReportSuccess("Tạo danh mục thành công", "post");
            return RespondSuccess(entityModel);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] CategoryPostModel entityModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //get  
            var category = _categoryService.Get(entityModel.Id);
            if (category == null)
                return RespondFailure();

            category.Name = entityModel.Name;          
            category.ParentCategoryId = entityModel.ParentCategoryId;
            category.Description = entityModel.Description;
            category.DisplayOrder = entityModel.DisplayOrder;
            category.UpdatedOnUtc = DateTime.Now;
            //save it
            _categoryService.Update(category);

            VerboseReporter.ReportSuccess("Sửa danh mục thành công", "put");
            return RespondSuccess(category);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id <= 0)
            {

                return RespondFailure();
            }
            _categoryService.Delete(x => x.Id == id);
            VerboseReporter.ReportSuccess("Xóa danh mục thành công", "delete");
            return RespondSuccess();
        }
    }
}