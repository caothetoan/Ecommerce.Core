using System;
using System.Collections.Generic;
using Vnit.ApplicationCore.Attributes;
using Vnit.ApplicationCore.Entities.Security;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.Users
{
   
    public partial class User : BaseEntity,  IHasEntityProperties<User>
    {
       
        public User()
        {
        }

        #region propertises

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

        [TokenField]
        public int? StudentYear { get; set; }

        [TokenField]
        public string  StudentCategory { get; set; }

        [TokenField]
        public string StudentUniversity { get; set; }

        #endregion

        #region navigation
        public virtual IList<UserRole> UserRoles { get; set; }


        ///// <summary>
        ///// Gets or sets shopping cart items
        ///// </summary>
        //public virtual ICollection<ShoppingCartItem> ShoppingCartItems
        //{
        //    get { return _shoppingCartItems ?? (_shoppingCartItems = new List<ShoppingCartItem>()); }
        //    protected set { _shoppingCartItems = value; }
        //}

        #endregion

    }
}
