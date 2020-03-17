using System;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.Pages
{
    // Page
    public class Page : BaseEntity, ISlugSupported, IHasLocalizedProperty<Page>
    {
        ///<summary>
        /// Tên trang
        ///</summary>
        public string Name { get; set; } // Name (length: 300)

        ///<summary>
        /// Mô tả về trang
        ///</summary>
        public string Description { get; set; } // Description (length: 500)

        ///<summary>
        /// Body html
        ///</summary>
        public string Body { get; set; } // ModuleName (length: 300)

        ///<summary>
        /// Trạng thái trang có được hiển thị trong menu hay không
        ///</summary>
        public bool ShowInMenu { get; set; } // ShowInMenu

        ///<summary>
        /// Thứ tự sắp xếp của trang
        ///</summary>
        public int DisplayOrder { get; set; } // OrderNo

        ///<summary>
        /// Biểu tượng của trang trong menu
        ///</summary>
        public string Icon { get; set; } // Icon

        ///<summary>
        /// Trạng thái đã xóa khỏi hệ thống của trang
        ///</summary>
        public bool IsDeleted { get; set; } // IsDeleted

        ///<summary>
        /// Đường dẫn của trang
        ///</summary>
        public string Url { get; set; } // Url (length: 300)

        ///<summary>
        /// Ngày tạo trang
        ///</summary>
        public DateTime CreateDate { get; set; } // CreateDate

        public bool Active { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; } // ModifyDate

        public int? ModifyBy { get; set; }
    }
}
