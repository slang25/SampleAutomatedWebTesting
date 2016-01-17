using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SampleAutomatedWebTesting.Startup))]
namespace SampleAutomatedWebTesting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
