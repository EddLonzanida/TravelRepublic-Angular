using Eml.ConfigParser.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using TravelRepublic.Infrastructure;

namespace TravelRepublic.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();

            try
            {
                var webHostBuilder = WebHostBuilder(args);

                Constants.CurrentEnvironment = webHostBuilder.GetSetting("environment");

				var loggerConnectionString = GetLoggerConnectionString(Constants.CurrentEnvironment);

                //Key should match Nlog.config key: connectionString = "${gdc:item=TravelRepublicConnectionString}"
                NLog.GlobalDiagnosticsContext.Set(ConnectionStrings.TravelRepublicDbKey, loggerConnectionString);

                webHostBuilder
                    .Build()
                    .Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "Stopped program because of unhandled exception.");
                throw;
            }
            finally
            {
                Eml.Mef.ClassFactory.Dispose(Startup.ClassFactory);
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder WebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).ConfigureLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddConsole();
                builder.AddDebug();
                builder.AddNLog();
            })
            .UseStartup<Startup>();

        private static string GetLoggerConnectionString(string currentEnvironment)
        {
           var configuration = ConfigBuilder.GetConfiguration(currentEnvironment)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            ConnectionStrings.SetOneTime(configuration);
            ApplicationSettings.SetOneTime(configuration);

            return ConnectionStrings.TravelRepublicDb;
        }
    }
}
