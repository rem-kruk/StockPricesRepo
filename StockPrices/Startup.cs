using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StockPrices.Startup))]
namespace StockPrices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
