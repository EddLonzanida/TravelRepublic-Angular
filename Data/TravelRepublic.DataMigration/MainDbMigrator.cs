using System.Composition;
using Eml.DataRepository;
using Eml.DataRepository.Attributes;
using TravelRepublic.DataMigration.BaseClasses;

namespace TravelRepublic.DataMigration
{
    [DbMigratorExport(Environments.PRODUCTION)]
    public class MainDbMigrator : MainDbMigratorBase<TravelRepublicDb>
    {
		[ImportingConstructor]
        public MainDbMigrator(MainDbConnectionString mainDbConnectionString) 
            :base(mainDbConnectionString)
        {
        }

        protected override void Seed(TravelRepublicDb context)
        {
        }
    }
}
