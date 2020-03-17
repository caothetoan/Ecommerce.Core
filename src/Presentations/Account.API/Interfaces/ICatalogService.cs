using Microsoft.AspNetCore.Mvc.Rendering;
using Vnit.Api.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vnit.ApplicationCore.Entities;

namespace Vnit.Api.Services
{
    public interface ICatalogService
    {
        Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId);
        Task<IEnumerable<SelectListItem>> GetBrands();
        Task<IEnumerable<SelectListItem>> GetTypes();

        Task<CatalogItem> GetItemById(int id);
    }
}
