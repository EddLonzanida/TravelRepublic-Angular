using Eml.ConfigParser.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TravelRepublic.Business.Common.Entities.TravelRepublicDb;
using TravelRepublic.Infrastructure;
using TravelRepublic.Infrastructure.Configurations;

namespace TravelRepublic.Data
{
    public class TravelRepublicDb : DbContext
    {
        public DbSet<Establishment> Establishments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = ConnectionStrings.TravelRepublicDb;

            if (string.IsNullOrWhiteSpace(connString))
            {
                var configuration = ConfigBuilder.GetConfiguration(Constants.CurrentEnvironment)
                    .AddJsonFile("appsettings.json")
                    .Build();

                using (var config = new TravelRepublicConnectionStringParser(configuration))
                {
                    connString = config.Value;
                }
            }

            optionsBuilder.UseSqlServer(connString);
        }
    }
}
//Add-Migration InitialCreate -OutputDir TravelRepublicDbMigrations -Context TravelRepublicDb
//Add-Migration InitialSeed -OutputDir TravelRepublicDbMigrations -Context TravelRepublicDb
//Update-Database -verbose -Context TravelRepublicDb
//Update-Database LastGoodMigration -verbose -Context TravelRepublicDb  // Revert a migration. Note: Migrations onwards will be deleted except LastGoodMigration

//Using console or terminal:
//navigate to TravelRepublic.Net folder
//dotnet ef migrations add InitialCreate -o TravelRepublicDbMigrations -c TravelRepublicDb -p Data/TravelRepublic.Net.DataMigration -s Hosts/TravelRepublic.Net.Api -v
//dotnet ef migrations add InitialStoredProcedures -o TravelRepublicDbMigrations -c TravelRepublicDb -p Data/TravelRepublic.Net.DataMigration -s Hosts/TravelRepublic.Net.Api -v
//dotnet ef database update -c TravelRepublicDb -p Data/TravelRepublic.Net.DataMigration -s Hosts/TravelRepublic.Net.Api -v
