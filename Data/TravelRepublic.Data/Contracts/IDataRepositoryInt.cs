using Eml.Contracts.Entities;
using Eml.DataRepository.Contracts;

namespace TravelRepublic.Data.Contracts
{
    public interface IDataRepositoryInt<T> : IDataRepositoryInt<T, TravelRepublicDb>
        where T : class, IEntityBase<int>
    {
    }
}
