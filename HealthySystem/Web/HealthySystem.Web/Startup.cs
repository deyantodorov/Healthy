using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealthySystem.Web.Startup))]
namespace HealthySystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
