using Vnit.ApplicationCore.Entities.OrderAggregate;
using System.Threading.Tasks;
using Vnit.ApplicationCore.Entities.Common;
using Vnit.ApplicationCore.Services;

namespace Vnit.ApplicationCore.Interfaces
{
    public interface IOrderService: IBaseEntityService<Order>
    {
        Task CreateOrderAsync(int basketId, Address shippingAddress);
    }
}
