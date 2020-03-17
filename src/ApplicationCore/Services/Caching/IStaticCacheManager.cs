using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Services.Caching
{
    /// <summary>
    /// Represents a manager for caching between HTTP requests (long term caching)
    /// </summary>
    public interface IStaticCacheManager : ICacheService
    {
    }
}
