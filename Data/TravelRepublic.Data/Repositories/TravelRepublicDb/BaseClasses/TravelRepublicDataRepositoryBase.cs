using Eml.Contracts.Entities;
using Eml.DataRepository.MsSql.BaseClasses;
using TravelRepublic.Infrastructure;
using TravelRepublic.Infrastructure.Contracts;

namespace TravelRepublic.Data.Repositories.TravelRepublicDb.BaseClasses
{
    public abstract class TravelRepublicDataRepositoryBase<TUniqueId, T> : DataRepositoryMsSqlBase<TUniqueId, T, Data.TravelRepublicDb>
        where T : class, IEntityBase<TUniqueId>, ITravelRepublicDbEntity
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
