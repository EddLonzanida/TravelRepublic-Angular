using Microsoft.Extensions.Configuration;
using TravelRepublic.Infrastructure.Configurations;

namespace TravelRepublic.Infrastructure
{
    /// <summary>
    /// Set in Startup.cs
    /// </summary>
    public static class ApplicationSettings
    {
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Set in Startup.cs
        /// </summary>
        public static ApplicationSettingsConfig Config { get; private set; }

        /// <summary>
        /// Set in Startup.cs
        /// </summary>
        public static void SetOneTime(IConfiguration configuration)
        {
            Configuration = configuration;

            using (var config = new ApplicationSettingsConfigParser(configuration))
            {
                Config = config.Value;
            }
        }
    }
}
