using System.ComponentModel.Composition;
using TravelRepublic.Contracts.Entities.Core;
using Eml.DataRepository;

namespace TravelRepublic.Data.Repositories
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepository<T> : DataRepositorySoftDeleteInt<T, TravelRepublicDb>
        where T : class, IEntityBase
    {
    }
}
