using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tasker.Startup))]
namespace Tasker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
