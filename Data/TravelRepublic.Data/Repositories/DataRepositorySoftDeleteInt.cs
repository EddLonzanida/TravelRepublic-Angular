using System.Composition;
using Eml.Contracts.Entities;
using Eml.DataRepository;
using Eml.DataRepository.Contracts;
using Microsoft.Extensions.Configuration;

namespace TravelRepublic.Data.Repositories
{
    [Export(typeof(IDataRepositorySoftDeleteInt<>))] //If using GUID, change to [Export(typeof(IDataRepositorySoftDeleteGuid<>))]
    public class DataRepositorySoftDeleteInt<T> : DataRepositorySoftDeleteInt<T, TravelRepublicDb>
        where T : class, IEntityBase<int>, IEntitySoftdeletableBase
    {
        [ImportingConstructor]
        public DataRepositorySoftDeleteInt(IConfiguration configuration) 
			: base(configuration)
        {
        }
    }
}
