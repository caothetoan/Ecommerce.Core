using System;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.Manufacturers
{
    public class Manufacturer : BaseEntity, IPermalinkSupported, ISlugSupported
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets a value ParentManufacturer 
        /// </summary>
        public int? ParentManufacturerId { get; set; }

        ///// <summary>
        ///// Gets or sets a value PageSize 
        ///// </summary>
        //public int? PageSize { get; set; }

        ///// <summary>
        ///// Gets or sets a value ShowOnHomePage
        ///// </summary>
        //public bool? ShowOnHomePage { get; set; }

        ///// <summary>
        ///// Gets or sets a value IncludeInTopMenu
        ///// </summary>
        //public bool? IncludeInTopMenu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime? UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the icon
        /// </summary>
        public string Icon { get; set; }
        #endregion

       
    }
}
