using System.ComponentModel.Composition;
using Eml.Contracts.Entities;
using Eml.DataRepository;

namespace TravelRepublic.Data.Repositories
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepository<T> : DataRepositorySoftDeleteInt<T, TravelRepublicDb>
        where T : class, IEntityBase<int>, IEntitySoftdeletableBase
    {
    }
}
