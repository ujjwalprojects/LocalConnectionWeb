using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute("LocalConnWebAPP",typeof(LocalConnWeb.Startup))]
namespace LocalConnWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
