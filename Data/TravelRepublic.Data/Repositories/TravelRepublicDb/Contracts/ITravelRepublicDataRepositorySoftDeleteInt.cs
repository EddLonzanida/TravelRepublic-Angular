using Eml.Contracts.Entities;
using Eml.DataRepository.MsSql.Contracts;
using TravelRepublic.Infrastructure.Contracts;

namespace TravelRepublic.Data.Repositories.TravelRepublicDb.Contracts
{
    public interface ITravelRepublicDataRepositorySoftDeleteInt<T> 
        : ITravelRepublicDataRepositoryInt<T>, IDataRepositoryMsSqlSoftDeleteInt<T, Data.TravelRepublicDb>
        where T : class, IEntityBase<int>, IEntitySoftdeletableBase, ITravelRepublicDbEntity
    {
    }
}
