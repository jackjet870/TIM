using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TIM.Web.Startup))]
namespace TIM.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
