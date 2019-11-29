using Eml.Contracts.Entities;
using TravelRepublic.Data.Repositories.TravelRepublicDb.BaseClasses;
using TravelRepublic.Data.Repositories.TravelRepublicDb.Contracts;
using TravelRepublic.Infrastructure.Contracts;
using System;
using System.Composition;

namespace TravelRepublic.Data.Repositories.TravelRepublicDb
{
    [Export(typeof(ITravelRepublicDataRepositoryInt<>))]
    public class TravelRepublicDataRepositoryInt<T> : TravelRepublicDataRepositoryBase<int, T>, ITravelRepublicDataRepositoryInt<T>
        where T : class, IEntityBase<int>, ITravelRepublicDbEntity
    {
    }
}
