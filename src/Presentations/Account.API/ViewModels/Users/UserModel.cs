using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vnit.ApplicationCore.Attributes;
using Vnit.ApplicationCore.Entities.Security;

namespace Vnit.Api.ViewModels.Users
{
    public class UserModel : RootModel
    {
        public Guid Guid { get; set; }

        [TokenField]
        public string FullName { get; set; }

        [TokenField]
        public string UserName { get; set; }

        public System.DateTime? BirthDay { get; set; }

        [TokenField]
        public string Email { get; set; }

        [TokenField]
        public string Phone { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public PasswordFormat PasswordFormat { get; set; }

        public string Address { get; set; }

        [TokenField]
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the last IP address
        /// </summary>
        public string LastIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last login
        /// </summary>
        public DateTime? LastLoginDateUtc { get; set; }
        /// <summary>
        /// Gets or sets a value indicating number of failed login attempts (wrong password)
        /// </summary>
        public int FailedLoginAttempts { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last activity
        /// </summary>
        public DateTime? LastActivityDateUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer account is system
        /// </summary>
        public bool IsSystemAccount { get; set; }

        /// <summary>
        /// Tài khoản quản trị
        /// </summary>
        public bool IsAdmin { get; set; }

        public bool Deleted { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Chứng minh thư
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// Loại khách hàng
        /// </summary>
        public int? CustomerType { get; set; }
        /// <summary>
        /// Tỉnh thành phố
        /// </summary>
        public int? ProvinceId { get; set; }

        [Display(Name = "Is administrator")]
        public bool IsAdministrator { get; set; }

        [Display(Name = "Is manager")]
        public bool IsManager { get; set; }

        [Required]
        [Display(Name = "Office Number")]
        public int OfficeNumber { get; set; }
    }
}
