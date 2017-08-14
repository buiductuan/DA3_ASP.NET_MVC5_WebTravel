using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Travel.Startup))]
namespace Travel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
