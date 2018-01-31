using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeFramework.Web.Startup))]
namespace WeFramework.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
