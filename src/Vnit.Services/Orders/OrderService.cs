using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities;
using Vnit.ApplicationCore.Entities.BasketAggregate;
using Vnit.ApplicationCore.Entities.Common;
using Vnit.ApplicationCore.Entities.OrderAggregate;
using Vnit.ApplicationCore.Interfaces;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.Orders
{
    public class OrderService : BaseEntityService<Order>, IOrderService
    {
        private readonly IAsyncRepository<Order> _orderRepository;
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public OrderService(IDataRepository<Order> dataRepository, IAsyncRepository<Basket> basketRepository,
            IAsyncRepository<CatalogItem> itemRepository,
            IAsyncRepository<Order> orderRepository) : base(dataRepository)
        {
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _itemRepository = itemRepository;
        }

        public async Task CreateOrderAsync(int basketId, Address shippingAddress)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);
            Guard.Against.NullBasket(basketId, basket);
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var catalogItem = await _itemRepository.GetByIdAsync(item.CatalogItemId);
                //var itemOrdered = new CatalogItemOrdered(catalogItem.Id, catalogItem.Name, catalogItem.PictureUri);
                //var orderItem = new OrderItem(itemOrdered, item.UnitPrice, item.Quantity);
                //items.Add(orderItem);
            }
            var order = new Order(basket.BuyerId, shippingAddress, items);

            await _orderRepository.AddAsync(order);
        }
    }
}
