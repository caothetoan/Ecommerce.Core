using System;
using System.Collections.Generic;
using Vnit.WebFramework.Models;

namespace Vnit.WebFramework.Models.Menus
{
   
    public class MenuModel : RootModel
    {

        #region main propertises
        public string Name { get; set; }

        public string Url { get; set; }

        public int Sequence { get; set; }

        public int? ParentId { get; set; }

        public int PositionId { get; set; }


        public int ParentLeft { get; set; }

        public int ParentRight { get; set; }

        public bool NewWindow { get; set; }

        public string Icon { get; set; }


        public bool? Active { get; set; }

        public bool? IsSystem { get; set; } 
        #endregion

        #region tracking propertises
        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int? ModifyBy { get; set; } 
        #endregion

        #region custom propertises
        public bool HasChildrent { get; set; }

        public bool HasParent { get; set; }

        public List<MenuModel> MenuChildrents { get; set; }

        public int LanguageId { get; set; }

        #endregion
    }
}