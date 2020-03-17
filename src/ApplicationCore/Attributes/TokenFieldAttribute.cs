using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Attributes
{
    /// <summary>
    /// Specifies a field that can be used for token replacements processing
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TokenFieldAttribute : Attribute
    {
    }
}
