using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Interfaces
{
    /// <summary>
    /// Represents an entity which supports slug (SEO friendly one-word URLs)
    /// </summary>
    public interface IMetaData
    {
        /// <summary>
        /// MetaTitle
        /// </summary>
        string MetaTitle { get; set; }

        ///<summary>
        /// MetaDescription
        ///</summary>
        string MetaDescription { get; set; }

        ///<summary>
        /// MetaKeywords
        ///</summary>
        string MetaKeywords { get; set; }
    }
}
