using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppNTCEcommerce2._0.Startup))]
namespace WebAppNTCEcommerce2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}