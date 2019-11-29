using Microsoft.Extensions.Configuration;
using TravelRepublic.Infrastructure.Configurations;

namespace TravelRepublic.Infrastructure
{
    /// <summary>
    /// Set in Startup.cs
    /// </summary>
    public static class ConnectionStrings
    {
        /// <summary>
        /// Should match the entry in appsettings*.json. Used in Program.cs
        /// </summary>
        public static string TravelRepublicDbKey { get; } = "TravelRepublicConnectionString";

        /// <summary>
        /// Set in Startup.cs
        /// </summary>
        public static string TravelRepublicDb { get; private set; }

        /// <summary>
        /// Set in Startup.cs
        /// </summary>
        public static void SetOneTime(IConfiguration configuration)
        {
            using (var config = new TravelRepublicConnectionStringParser(configuration))
            {
                TravelRepublicDb = config.Value;
            }
        }
    }
}
