using Eml.Contracts.Entities;
using Eml.DataRepository.Contracts;

namespace TravelRepublic.Data.Contracts
{
    public interface IDataRepositorySoftDeleteInt<T> : IDataRepositorySoftDeleteInt<T, TravelRepublicDb>
        where T : class, IEntityBase<int>, IEntitySoftdeletableBase
    {
    }
}
