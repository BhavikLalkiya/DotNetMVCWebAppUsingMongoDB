using System.Web.Http;

namespace DotNetMVCWebAppUsingMongoDB
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            config.Routes.MapHttpRoute("API Default action", "api/{controller}/{action}/{id}",
                new { controller = "", action = "", id = RouteParameter.Optional });
        }
    }
}
