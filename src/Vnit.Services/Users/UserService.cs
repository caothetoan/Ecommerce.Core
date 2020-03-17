using System.Linq;
using System.Threading.Tasks;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Users;
using Vnit.ApplicationCore.Enums;

namespace Vnit.ApplicationCore.Services.Users
{
    public class UserService : BaseEntityService<User>, IUserService
    {
        public UserService(IDataRepository<User> dataRepository) : base(dataRepository)
        {
        }

        public UserLoginStatus LogIn(string username, string hashedPassword)
        {

            var user = Repository.Get(e => 
            (e.UserName.Equals(username) || e.Email.Equals(username))
            && e.Password.Equals(hashedPassword)
                                           && !e.Deleted)
                                           .FirstOrDefault();
            if (user == null)
                return UserLoginStatus.UserOrPasswordInCorrect;

            if (user.Deleted)
                return UserLoginStatus.FailedDeletedUser;

            if (!user.Active)
                return UserLoginStatus.FailedInactiveUser;

            return UserLoginStatus.Success;
        }
        /// <summary>
        /// Get user by user name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User Get(string username)
        {
            return Repository.Get(e =>
            (e.UserName.Equals(username) || e.Email.Equals(username))
            && !e.Deleted).FirstOrDefault();
        }

        public User Get(string username, bool asNoTracking)
        {
            return Repository.Get(e =>
                (e.UserName.Equals(username) || e.Email.Equals(username))
                && !e.Deleted,
                asNoTracking).FirstOrDefault();
        }

        public async Task<User> GetAsync(string username)
        {
            var taskQueryable = await Repository.GetAsync(e =>
            (e.UserName.Equals(username) || e.Email.Equals(username))
            && !e.Deleted);

            return taskQueryable.FirstOrDefault();
        }
    }
}
