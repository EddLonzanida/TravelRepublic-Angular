using System.Data.Entity.Migrations;
using TravelRepublic.Data;
using TravelRepublic.Data.Migrations;

namespace TravelRepublic.Tests.Integration.Migrations
{
    public class IntegrationTestConfiguration: DbMigrationsConfiguration<TravelRepublicDb>
    {
        private const string JSON_SOURCES = @"Migrations\JsonSources";

        public IntegrationTestConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TravelRepublicDb context)
        {
            HotelData.Seed(context,JSON_SOURCES);
        }
    }
}
