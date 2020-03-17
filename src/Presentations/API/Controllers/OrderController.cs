using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Entities.OrderAggregate;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Interfaces;
using Vnit.WebFramework.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

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
            {
                requestModel = new RootRequestModel().DefaultRequest();
            }

            if (requestModel.Page < 1)
                requestModel.Page = 1;
            if (requestModel.Count < 1)
                requestModel.Count = 10;

            Expression<Func<Order, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd(where, a => a.BuyerId.Contains(requestModel.Name));

            var orders = await _orderService.GetPagedListAsync(
                where,
                x => x.OrderDate,
                true,
                requestModel.Page - 1,
                requestModel.Count);
            if (orders == null)
                return RespondFailure();
            var model = orders;//.Select(x => x.ToModel());

            return RespondSuccess(model, orders.TotalCount);
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
            var order = await _orderService.FirstOrDefaultAsync(x => x.Id == id);
            return RespondSuccess(order);
        }

    }
}
