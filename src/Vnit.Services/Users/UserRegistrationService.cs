using System;
using Vnit.ApplicationCore.Entities.Security;
using Vnit.ApplicationCore.Entities.Users;
using Vnit.ApplicationCore.Enums;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.Users;
using Vnit.Services.Security;

namespace Vnit.Services.Users
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserService _userService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IWebHelper _webHelper;

        public UserRegistrationService(IUserService userService,
            ICryptographyService cryptographyService, 
            IWebHelper webHelper)
        {
            _userService = userService;
            _cryptographyService = cryptographyService;
            _webHelper = webHelper;
        }

        public UserRegistrationStatus Register(string email, string password, PasswordFormat passwordFormat)
        {
            //before registering the user, we need to check a few things

            //does the user exist already?
            var existingUser = _userService.FirstOrDefault(x => x.Email == email);
            if (existingUser != null)
                return UserRegistrationStatus.FailedAsEmailAlreadyExists;

            //we can create a user now, we'll need to hash the password
            var salt = _cryptographyService.CreateSalt(8); //64 bits...should be good enough

            var hashedPassword = _cryptographyService.GetHashedPassword(password, salt, passwordFormat);

            //create a new user entity
            var user = new User()
            {
                Email = email,
                UserName = email,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
                LastLoginDateUtc = DateTime.UtcNow,
                LastIpAddress = _webHelper.GetCurrentIpAddress(),
                Password = hashedPassword,
                PasswordSalt = salt,
                PasswordFormat = passwordFormat,
                IsAdmin = false,
                IsSystemAccount = false,
                Active = true,
                Guid = Guid.NewGuid()
            };

            _userService.Insert(user);
            return UserRegistrationStatus.Success;
        }

        public UserRegistrationStatus Register(User user, PasswordFormat passwordFormat)
        {
            //does the user exist already?
            var existingUser = _userService.FirstOrDefault(x => x.Email == user.Email);
            if (existingUser != null)
                return UserRegistrationStatus.FailedAsEmailAlreadyExists;

            //we can create a user now, we'll need to hash the password
            var salt = _cryptographyService.CreateSalt(8); //64 bits...should be good enough

            var hashedPassword = _cryptographyService.GetHashedPassword(user.Password, salt, passwordFormat);

            user.Password = hashedPassword;
            user.PasswordSalt = salt;
            user.PasswordFormat = passwordFormat;
            _userService.Insert(user);
            return UserRegistrationStatus.Success;
        }
    }
}
