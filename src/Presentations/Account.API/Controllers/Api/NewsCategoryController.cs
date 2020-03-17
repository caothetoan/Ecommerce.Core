using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.News;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.News;
using Vnit.Api.Extensions;
using Vnit.Api.ViewModels.News;

namespace Vnit.Api.Controllers.Api
{
    public class NewsCategoryController : BaseApiController
    {
        private readonly INewsCategoryService _newsCategoryService;

        public NewsCategoryController(INewsCategoryService newsCategoryService)
        {
            _newsCategoryService = newsCategoryService;
        }

       
        /// <summary>
        /// Lấy ds danh mục
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get([FromQuery] NewsCategoryRequestModel requestModel)
        {
            #region predicate
            if (requestModel == null)
                return BadRequest();

            if (requestModel.Page < 1)
                requestModel.Page = 1;
            if (requestModel.Count < 1)
                requestModel.Count = 10;
            Expression<Func<NewsCategory, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = a => a.Name.Contains(requestModel.Name);

            if (requestModel.IsGetParent.HasValue)
            {
                where = ExpressionHelpers.CombineAnd<NewsCategory>(where, x => x.ParentId == null || x.ParentId == 0);
            }
           
            bool ascending = true;

            if (requestModel.Ascending.HasValue)
                ascending = requestModel.Ascending.Value;
            #endregion

            IPagedList<NewsCategory> categories;

            switch (requestModel.OrderBy)
            {
                case "Name":
                    categories = await _newsCategoryService.GetPagedListAsync(
                        where,
                        x => x.Name,
                        ascending,
                        requestModel.Page - 1,
                        requestModel.Count);

                    break;
                default:
                    categories = await _newsCategoryService.GetPagedListAsync(
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

        [HttpGet]
        public IActionResult GetTreeview([FromQuery] NewsCategoryRequestModel requestModel)
        {
            #region predicate
            Expression<Func<NewsCategory, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = a => a.Name.Contains(requestModel.Name);

           
            if (requestModel.IsGetParent.HasValue && requestModel.IsGetParent.Value)
            {
                where = ExpressionHelpers.CombineAnd<NewsCategory>(where, x => x.ParentId == null || x.ParentId == 0);
            }
            bool ascending = false;
            if (requestModel.Ascending.HasValue)
                ascending = requestModel.Ascending.Value;

            #endregion

            var categories = _newsCategoryService.GetPagedList(
                 where,
                 null,
                 ascending,
                 requestModel.Page - 1,
                 requestModel.Count);

            var newsCategoryModels = categories.ToList().ToParentModels();
            
            return RespondSuccess(newsCategoryModels, newsCategoryModels.Count);
        }

        [HttpGet]
        [Route("parents")]
        public IActionResult GetParents([FromQuery] NewsCategoryRequestModel requestModel)
        {
            #region predicate
            Expression<Func<NewsCategory, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = a => a.Name.Contains(requestModel.Name);

         
            if (requestModel.IsGetParent.HasValue && requestModel.IsGetParent.Value)
            {
                where = ExpressionHelpers.CombineAnd<NewsCategory>(where, x => x.ParentId == null || x.ParentId == 0);
            }
            bool ascending = false;
            if (requestModel.Ascending.HasValue)
                ascending = requestModel.Ascending.Value;

            #endregion

            var categories = _newsCategoryService.GetPagedList(
                 where,
                 null,
                 ascending,
                 requestModel.Page - 1,
                 requestModel.Count);

            var newsCategoryModels = categories.ToList().ToParentModels();
            
            return RespondSuccess(newsCategoryModels, newsCategoryModels.Count);
        }

        [Route("get/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            #region predicate
            Expression<Func<NewsCategory, bool>> where = x => true;

           
            where = ExpressionHelpers.CombineAnd(where, x => x.Id == id); 
            #endregion

            var newsCategory = await _newsCategoryService.FirstOrDefaultAsync(where);
            if (newsCategory == null)
                return RespondFailure();

            return RespondSuccess(newsCategory.ToModel());
        }

        [Route("post")]
        [HttpPost]
        public IActionResult Post(NewsCategoryModel entityModel)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            var newsCategory = entityModel.ToEntity();
            //save it
            _newsCategoryService.Insert(newsCategory);

            VerboseReporter.ReportSuccess("Tạo danh mục thành công", "post");
            return RespondSuccess(entityModel
            );
        }

        [Route("put")]
        [HttpPost]
        public IActionResult Put(NewsCategoryModel entityModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            //get  
            var newsCategory = _newsCategoryService.Get(entityModel.Id);
            if (newsCategory == null)
                return RespondFailure();

            newsCategory.Name = entityModel.Name;
          
            newsCategory.ParentId = entityModel.ParentId;
            newsCategory.Short = entityModel.Short;
            newsCategory.DisplayOrder = entityModel.DisplayOrder;
            //save it
            _newsCategoryService.Update(newsCategory);

            VerboseReporter.ReportSuccess("Sửa danh mục thành công", "put");
            return RespondSuccess(newsCategory);
        }

        [Route("delete/{id:int}")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {

                return RespondFailure();
            }
            _newsCategoryService.Delete(x => x.Id == id);
            VerboseReporter.ReportSuccess("Xóa danh mục thành công", "delete");
            return RespondSuccess();
        }
    }
}