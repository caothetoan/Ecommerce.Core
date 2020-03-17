using Vnit.Api.ViewModels;
using System.Threading.Tasks;

namespace Vnit.Api.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    }
}
