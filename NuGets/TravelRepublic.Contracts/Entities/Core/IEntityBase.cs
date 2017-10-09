using Eml.Contracts.Entities;

namespace TravelRepublic.Contracts.Entities.Core
{
    public interface IEntityBase : IEntityBase<int>, IEntitySoftdeletableBase
    {
    }
}
