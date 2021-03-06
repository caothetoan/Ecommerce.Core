﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Vnit.Api.ViewModels;
using System;
using Vnit.ApplicationCore.Entities.OrderAggregate;
using Vnit.ApplicationCore.Interfaces;
using System.Linq;
using Vnit.ApplicationCore.Specifications;

namespace Vnit.Api.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository) {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepository.GetAsync(new CustomerOrdersWithItemsSpecification(User.Identity.Name));

            var viewModel = orders
                .Select(o => new OrderViewModel()
                {
                    OrderDate = o.OrderDate,
                    OrderItems = o.OrderItems?.Select(oi => new OrderItemViewModel()
                    {
                        Discount = 0,
                        //PictureUrl = oi.ItemOrdered.PictureUri,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        UnitPrice = oi.UnitPriceInclTax,
                        Units = oi.Quantity
                    }).ToList(),
                    OrderNumber = o.Id,
                    ShippingAddress = o.ShippingAddress,
                    Status = "Pending",
                    Total = o.Total()

                });
            return View(viewModel);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Detail(int orderId)
        {
            var customerOrders = await _orderRepository.GetAsync(new CustomerOrdersWithItemsSpecification(User.Identity.Name));
            var order = customerOrders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return BadRequest("No such order found for this user.");
            }
            var viewModel = new OrderViewModel()
            {
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel()
                {
                    Discount = 0,
                    //PictureUrl = oi.ItemOrdered.PictureUri,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    UnitPrice = oi.UnitPriceInclTax,
                    Units = oi.Quantity
                }).ToList(),
                OrderNumber = order.Id,
                ShippingAddress = order.ShippingAddress,
                Status = "Pending",
                Total = order.Total()
            };
            return View(viewModel);
        }
    }
}
