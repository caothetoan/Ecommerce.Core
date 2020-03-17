using System;
using System.Collections.Generic;
using System.Linq;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Catalog;
using Vnit.ApplicationCore.Entities.News;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.Catalogs
{
   
    public class ProductService : EntityService<Product>, IProductService
    {
        private readonly IDataRepository<ProductCategory> _productCategoryRepository;
        private readonly IDataRepository<Category> _categoryRepository;

        public ProductService(IDataRepository<Product> dataRepository, IDataRepository<ProductCategory> productCategoryRepository, IDataRepository<Category> categoryRepository) : base(dataRepository)
        {
            _productCategoryRepository = productCategoryRepository;
            _categoryRepository = categoryRepository;
        }



        public void Add(Product product, int[] productCategoryIds)
        {
            base.Insert(product);

            if (productCategoryIds != null && productCategoryIds.Length > 0)
            {

                foreach (int productCategoryId in productCategoryIds)
                {
                    AttachProductToCategory(product.Id, productCategoryId);
                }
            }
        }
        public void AttachProductToCategory(int productId, int categoryId)
        {
            if (productId == 0)
            {
                throw new Exception("Can't attach product with media with Id '0'");
            }
            //insert product picture only if it doesn't exist
            var insertRequired =
                !_productCategoryRepository.Get(x => x.ProductId == productId && x.CategoryId == categoryId).Any();

            if (!insertRequired) return;


            var entityMedia = new ProductCategory()
            {
                ProductId = productId,
                CategoryId = categoryId,                
                DisplayOrder = 0
            };

            _productCategoryRepository.Insert(entityMedia);
        }

        public void AttachProductToCategory(Product product, Category category)
        {
            if (category.Id == 0)
            {
                _categoryRepository.Insert(category);
            }
            AttachProductToCategory(product.Id, category.Id);
        }



        public void UnassignProductToCategory(Product product, Category category)
        {
            var productFound = GetProductsByCategoryId(category.Id).FirstOrDefault(x => x.Id == product.Id);
            if (productFound == null)
                return;

            _productCategoryRepository.Delete(x => x.ProductId == product.Id && x.CategoryId == category.Id);

        }

        public void UnassignProductToCategory(int productId, int categoryId)
        {
            var product = GetProductsByCategoryId(categoryId).FirstOrDefault(x => x.Id == productId);
            if (product == null)
                return;

            _productCategoryRepository.Delete(x => x.ProductId == productId && x.CategoryId == categoryId);
        }

        public IList<Product> GetProductsByCategoryId(int categoryId)
        {
            return _productCategoryRepository.Get(x => x.CategoryId == categoryId).Select(x => x.Product).ToList();
        }
    }
}
