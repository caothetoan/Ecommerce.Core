using Vnit.ApplicationCore.Entities;

namespace Vnit.ApplicationCore.Interfaces
{
    public interface IHasEntityProperties<T> : IHasEntityProperties where T : BaseEntity
    {

    }

    public interface IHasEntityProperties
    {
        int Id { get; set; }
    }
}
