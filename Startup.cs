using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuotationsApp.Startup))]
namespace QuotationsApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
