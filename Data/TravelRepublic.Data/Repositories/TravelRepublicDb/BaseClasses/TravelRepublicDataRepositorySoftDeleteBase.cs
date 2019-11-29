using Eml.Contracts.Entities;
using Eml.DataRepository.MsSql.BaseClasses;
using TravelRepublic.Infrastructure;
using TravelRepublic.Infrastructure.Contracts;

namespace TravelRepublic.Data.Repositories.TravelRepublicDb.BaseClasses
{
    public abstract class TravelRepublicDataRepositorySoftDeleteBase<TUniqueId, T> : DataRepositoryMsSqlSoftDeleteBase<TUniqueId, T, Data.TravelRepublicDb>
        where T : class, IEntityBase<TUniqueId>, IEntitySoftdeletableBase, ITravelRepublicDbEntity
    {
        public override int GetIntellisenseSize()
        {
            return ApplicationSettings.Config.IntellisenseCount;
        }

        public override string GetConnectionString()
        {
            return ConnectionStrings.TravelRepublicDbKey;
        }

        public override int GetPageSize()
        {
            return ApplicationSettings.Config.PageSize;
        }
    }
}
