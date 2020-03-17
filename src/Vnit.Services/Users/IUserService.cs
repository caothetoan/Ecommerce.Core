using System.Threading.Tasks;
using Vnit.ApplicationCore.Entities.Users;
using Vnit.ApplicationCore.Enums;

namespace Vnit.ApplicationCore.Services.Users
{
    public interface IUserService : IBaseEntityService<User>
    {
        UserLoginStatus LogIn(string username, string hashedPassword);

        User Get(string username);

        User Get(string username, bool asNoTracking);

        Task<User> GetAsync(string username);
    }
}
