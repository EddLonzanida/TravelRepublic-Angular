using System.Data.Entity.Migrations;

namespace TravelRepublic.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TravelRepublicDb>
    {
        private const string JSON_SOURCES = @"Migrations\JsonSources";

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
            HotelData.Seed(context,JSON_SOURCES);
        }
    }
}
