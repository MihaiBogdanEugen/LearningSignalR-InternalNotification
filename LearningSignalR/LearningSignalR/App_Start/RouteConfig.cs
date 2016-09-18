using System.Web.Mvc;
using System.Web.Routing;

namespace LearningSignalR
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            #region Session Routes

            routes.MapRoute(name: "Login", url: "Login", defaults: new
            {
                controller = "Session",
                action = "Login"
            });
            routes.MapRoute(name: "Logout", url: "Logout", defaults: new
            {
                controller = "Session",
                action = "Logout"
            });

            #endregion Session Routes

            routes.MapRoute(name: "Default", url: "{controller}/{action}/{id}", defaults: new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            });
        }
    }
}
