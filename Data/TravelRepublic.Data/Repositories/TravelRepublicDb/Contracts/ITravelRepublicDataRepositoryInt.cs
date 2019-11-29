using Eml.Contracts.Entities;
using Eml.DataRepository.MsSql.Contracts;
using TravelRepublic.Infrastructure.Contracts;
using System;

namespace TravelRepublic.Data.Repositories.TravelRepublicDb.Contracts
{
    public interface ITravelRepublicDataRepositoryInt<T> : IDataRepositoryMsSqlInt<T, Data.TravelRepublicDb>
        where T : class, IEntityBase<int>, ITravelRepublicDbEntity
    {
    }
}
