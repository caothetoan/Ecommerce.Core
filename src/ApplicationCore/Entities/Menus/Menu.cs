using System;
using System.Collections.Generic;
using System.Text;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.Menus
{
    public class Menu : BaseEntity, ITracking, IHasLocalizedProperty<Menu>
    {
        #region propertises
        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }

        public string Url { get; set; }

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int Sequence { get; set; }

        public int? ParentId { get; set; }

        /// <summary>
        /// Vị trí
        /// </summary>
        public int PositionId { get; set; }


        public bool NewWindow { get; set; }

        public string Icon { get; set; }


        public bool? Active { get; set; }
        /// <summary>
        /// Menu hệ thống
        /// </summary>
        public bool? IsSystem { get; set; }
        #endregion

        #region tracking propertises
        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int? ModifyBy { get; set; }
        #endregion
    }
}
