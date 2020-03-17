using Vnit.RazorPages.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vnit.RazorPages.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    }
}
