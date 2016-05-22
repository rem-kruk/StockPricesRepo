using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Prices.Startup))]
namespace Prices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
