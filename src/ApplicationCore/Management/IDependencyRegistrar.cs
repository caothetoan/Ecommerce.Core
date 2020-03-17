using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vnit.ApplicationCore.Configuration;

namespace Vnit.ApplicationCore.Management
{
    /// <summary>
    /// Dependency registrar interface
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        /// <param name="configuration"></param>
        void Register(IServiceCollection builder, ITypeFinder typeFinder, NopConfig config, IConfiguration configuration);

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        int Order { get; }
    }
}
