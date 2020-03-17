using System;
using System.ComponentModel.DataAnnotations;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.Widgets
{
    public class GroupPage : BaseEntity, IHasLocalizedProperty<GroupPage>
    {
        ///<summary>
        /// Tên module
        ///</summary>
        public string Name { get; set; } // Name (length: 300)

        ///<summary>
        /// Icon hiển thị của module
        ///</summary>
        public string Icon { get; set; } // Icon (length: 300)

        ///<summary>
        /// Mô tả về module
        ///</summary>
        public string Description { get; set; } // Description (length: 500)

        ///<summary>
        /// Body html
        ///</summary>
        public string Body { get; set; } // ModuleName (length: 300)

        ///<summary>
        /// Thứ tự của module
        ///</summary>
        public int OrderNo { get; set; } // OrderNo

        ///<summary>
        /// Id của module cha
        ///</summary>
        public int? ParentId { get; set; } // ParentId

        ///<summary>
        /// Cấp của module (bắt đầu từ 0)
        ///</summary>
        public byte Level { get; set; } // Level

        ///<summary>
        /// ActionName 
        ///</summary>
        [Required]
        public string ActionName { get; set; }

        ///<summary>
        /// ControllerName
        ///</summary>
        [Required]
        public string ControllerName { get; set; } // ControllerName (length: 1000)

        ///<summary>
        /// Trạng thái xóa khỏi hệ thống của module
        ///</summary>
        public bool IsDeleted { get; set; } // IsDeleted

        ///<summary>
        /// Ngày tạo
        ///</summary>
        public DateTime CreateDate { get; set; } // CreateDate

        public int? CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; } // ModifyDate

        public int? ModifyBy { get; set; }

        public string UnsignedName { get; set; } // UnsignedName (length: 100)

        public bool Active { get; set; }

        ///<summary>
        /// Đường dẫn của trang
        ///</summary>
        public string Url { get; set; } // Url (length: 300)

        /// <summary>
        /// Mã dịch vụ
        /// </summary>
        public int? ServiceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsSystem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Version { get; set; }

        ///<summary>
        /// anh nền
        ///</summary>
        public string Cover { get; set; }

        /// <summary>
        /// Css style inline
        /// </summary>
        public string Style { get; set; }
    }
}
