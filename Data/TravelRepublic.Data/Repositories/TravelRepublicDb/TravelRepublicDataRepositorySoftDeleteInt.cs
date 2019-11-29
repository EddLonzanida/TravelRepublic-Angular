using Eml.Contracts.Entities;
using TravelRepublic.Data.Repositories.TravelRepublicDb.BaseClasses;
using TravelRepublic.Data.Repositories.TravelRepublicDb.Contracts;
using TravelRepublic.Infrastructure.Contracts;
using System;
using System.Composition;

namespace TravelRepublic.Data.Repositories.TravelRepublicDb
{
    [Export(typeof(ITravelRepublicDataRepositorySoftDeleteInt<>))]
    public class TravelRepublicDataRepositorySoftDeleteInt<T> : TravelRepublicDataRepositorySoftDeleteBase<int, T>, ITravelRepublicDataRepositorySoftDeleteInt<T>
        where T : class, IEntityBase<int>, IEntitySoftdeletableBase, ITravelRepublicDbEntity
    {
    }
}
