using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NET.Web.UI.Startup))]
namespace NET.Web.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
