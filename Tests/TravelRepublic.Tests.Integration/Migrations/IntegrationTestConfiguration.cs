using System.Data.Entity.Migrations;
using TravelRepublic.Data;
using TravelRepublic.Data.Migrations;
using TravelRepublic.Data.Migrations.Seeders;

namespace TravelRepublic.Tests.Integration.Migrations
{
    public class IntegrationTestConfiguration : DbMigrationsConfiguration<TravelRepublicDb>
    {
        private const string SAMPLE_DATA_SOURCES = @"Migrations\SampleDataSources";

        public IntegrationTestConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TravelRepublicDb context)
        {
            HotelSeeder.Seed(context, SAMPLE_DATA_SOURCES);
        }
    }
}
