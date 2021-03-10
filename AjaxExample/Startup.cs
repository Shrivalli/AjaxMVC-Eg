using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AjaxExample.Startup))]
namespace AjaxExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
