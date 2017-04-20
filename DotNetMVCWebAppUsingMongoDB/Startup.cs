using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DotNetMVCWebAppUsingMongoDB.Startup))]
namespace DotNetMVCWebAppUsingMongoDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
