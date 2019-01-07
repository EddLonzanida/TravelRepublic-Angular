using System.Composition;
using Eml.Contracts.Entities;
using Eml.DataRepository;
using Microsoft.Extensions.Configuration;
using TravelRepublic.Data.Contracts;

namespace TravelRepublic.Data.Repositories
{
    [Export(typeof(IDataRepositoryInt<>))] 
    public class DataRepositoryInt<T> : DataRepositoryInt<T, TravelRepublicDb>, IDataRepositoryInt<T>
        where T : class, IEntityBase<int>
    {
        [ImportingConstructor]
        public DataRepositoryInt(IConfiguration configuration) 
			: base(configuration)
        {
        }
    }
}
