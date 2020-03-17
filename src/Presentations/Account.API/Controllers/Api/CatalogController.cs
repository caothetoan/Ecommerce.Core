using System.Net;
using Vnit.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vnit.ApplicationCore.Entities;

namespace Vnit.Api.Controllers.Api
{
    public class CatalogController : BaseApiController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService) => _catalogService = catalogService;

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get(int? brandFilterApplied, int? typesFilterApplied, int? page)
        {
            var itemsPage = 10;           
            var catalogModel = await _catalogService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied);
            return Ok(catalogModel);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var item = await _catalogService.GetItemById(id);

          
            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

    }
}
