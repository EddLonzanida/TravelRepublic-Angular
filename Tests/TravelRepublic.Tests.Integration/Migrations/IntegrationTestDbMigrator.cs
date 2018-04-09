using System.ComponentModel.Composition;
using Eml.DataRepository;
using Eml.DataRepository.Attributes;
using TravelRepublic.Data;
using Eml.ConfigParser;
using Eml.DataRepository.BaseClasses;

namespace TravelRepublic.Tests.Integration.Migrations
{
    [DbMigratorExport(Environments.INTEGRATIONTEST)]
    public class IntegrationTestDbMigrator : MigratorBase<TravelRepublicDb, IntegrationTestConfiguration>
    {
        [ImportingConstructor]
        public IntegrationTestDbMigrator(IConfigBase<string, MainDbConnectionString> mainDbConnectionString)
            :base(mainDbConnectionString.Value)
        {
        }
    }
}