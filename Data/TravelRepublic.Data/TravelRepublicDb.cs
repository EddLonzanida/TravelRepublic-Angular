using Eml.ConfigParser.Helpers;
using Eml.DataRepository;
using Microsoft.EntityFrameworkCore;
using TravelRepublic.Business.Common.Entities;

namespace TravelRepublic.Data
{
    public class TravelRepublicDb : DbContext
    {
        public DbSet<Establishment> Establishments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ConfigBuilder.GetConfiguration();
            var mainDbConnectionString = new MainDbConnectionString(config);

            optionsBuilder.UseSqlServer(mainDbConnectionString.Value);
        }
    }
}
