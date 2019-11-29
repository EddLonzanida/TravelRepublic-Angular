using Eml.DataRepository.Attributes;
using TravelRepublic.Infrastructure;
using Eml.DataRepository.BaseClasses;

namespace TravelRepublic.DataMigration.Migrators
{
    [DbMigratorExport(DbNames.TravelRepublic)]
    public class TravelRepublicDbMigrator : MigratorBase<TravelRepublicDb>
    {
    }
}
