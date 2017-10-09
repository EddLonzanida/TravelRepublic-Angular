using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using TravelRepublic.ApiHost;

[assembly: OwinStartup(typeof(Startup))]
namespace TravelRepublic.ApiHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        private static void ConfigureAuth(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}