using System.Data.Entity.Migrations;
using TravelRepublic.Data.Migrations.Seeders;

namespace TravelRepublic.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TravelRepublicDb>
    {
        private const string SAMPLE_DATA_SOURCES = @"Migrations\SampleDataSources";

        public Configuration()
        {
            var isEnabled = false; //Disable if running in Release Mode
#if DEBUG
            isEnabled = true;
#endif
            AutomaticMigrationsEnabled = isEnabled;
            AutomaticMigrationDataLossAllowed = isEnabled;
        }

        protected override void Seed(TravelRepublicDb context)
        {
            HotelSeeder.Seed(context, SAMPLE_DATA_SOURCES);
        }
    }
}
