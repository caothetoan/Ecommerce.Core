using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vnit.Api.ViewModels;
using Vnit.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using Vnit.ApplicationCore.Interfaces;
using System;
using Vnit.ApplicationCore.Specifications;

namespace Vnit.Api.Services
{
    /// <summary>
    /// This is a UI-specific service so belongs in UI project. It does not contain any business logic and works
    /// with UI-specific types (view models and SelectListItem types).
    /// </summary>
    public class CatalogService : ICatalogService
    {
        private readonly ILogger<CatalogService> _logger;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;
        private readonly IAsyncRepository<CatalogBrand> _brandRepository;
        private readonly IAsyncRepository<CatalogType> _typeRepository;
        private readonly IUriComposer _uriComposer;

        public CatalogService(
            ILoggerFactory loggerFactory,
            IAsyncRepository<CatalogItem> itemRepository,
            IAsyncRepository<CatalogBrand> brandRepository,
            IAsyncRepository<CatalogType> typeRepository,
            IUriComposer uriComposer)
        {
            _logger = loggerFactory.CreateLogger<CatalogService>();
            _itemRepository = itemRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _uriComposer = uriComposer;
        }


        public async Task<CatalogItem> GetItemById(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);

            //var baseUri = _settings.PicBaseUrl;
            //var azureStorageEnabled = _settings.AzureStorageEnabled;
            //item.FillProductUrl(baseUri, azureStorageEnabled: azureStorageEnabled);
            return item;
        }

        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId)
        {
            _logger.LogInformation("GetCatalogItems called.");

            var filterSpecification = new CatalogFilterSpecification(brandId, typeId);
            var catalogItems = await _itemRepository.GetAsync(filterSpecification);

            if (catalogItems != null)
            {
                var totalItems = catalogItems.Count();

                var itemsOnPage = catalogItems
                    .Skip(itemsPage * pageIndex)
                    .Take(itemsPage)
                    .ToList();

                itemsOnPage.ForEach(x =>
                {
                    x.PictureUri = _uriComposer.ComposePicUri(x.PictureUri);
                });

                var viewModel = new CatalogIndexViewModel()
                {
                    CatalogItems = itemsOnPage.Select(i => new CatalogItemViewModel()
                    {
                        Id = i.Id,
                        Name = i.Name,
                        PictureUri = i.PictureUri,
                        Price = i.Price
                    }),
                    Brands = await GetBrands(),
                    Types = await GetTypes(),
                    BrandFilterApplied = brandId ?? 0,
                    TypesFilterApplied = typeId ?? 0,
                    PaginationInfo = new PaginationInfoViewModel()
                    {
                        ActualPage = pageIndex,
                        ItemsPerPage = itemsOnPage.Count,
                        TotalItems = totalItems,
                        TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                    }
                };

                viewModel.PaginationInfo.Next = (viewModel.PaginationInfo.ActualPage == viewModel.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
                viewModel.PaginationInfo.Previous = (viewModel.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

                return viewModel;
            }

            return null;
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            _logger.LogInformation("GetBrands called.");
            var brands = await _brandRepository.GetAllAsync();

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (CatalogBrand brand in brands)
            {
                items.Add(new SelectListItem() { Value = brand.Id.ToString(), Text = brand.Brand });
            }

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            _logger.LogInformation("GetTypes called.");
            var types = await _typeRepository.GetAllAsync();
            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (CatalogType type in types)
            {
                items.Add(new SelectListItem() { Value = type.Id.ToString(), Text = type.Type });
            }

            return items;
        }
    }
}
