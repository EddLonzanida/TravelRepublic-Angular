using Eml.Contracts.Entities;
using Eml.DataRepository;
using Microsoft.Extensions.Configuration;
using System.Composition;
using TravelRepublic.Data.Contracts;

namespace TravelRepublic.Data.Repositories
{
    [Export(typeof(IDataRepositorySoftDeleteInt<>))]
    public class DataRepositorySoftDeleteInt<T> : DataRepositorySoftDeleteInt<T, TravelRepublicDb>, IDataRepositorySoftDeleteInt<T>
        where T : class, IEntityBase<int>, IEntitySoftdeletableBase
    {
        [ImportingConstructor]
        public DataRepositorySoftDeleteInt(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
