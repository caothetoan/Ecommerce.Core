using System.Security.Principal;

namespace Vnit.ApplicationCore.Interfaces
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
