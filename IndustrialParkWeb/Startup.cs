using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IndustrialParkWeb.Startup))]
namespace IndustrialParkWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
