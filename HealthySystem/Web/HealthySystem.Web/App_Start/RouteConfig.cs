namespace HealthySystem.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Rubric",
                url: "r/{alias}",
                defaults: new { controller = "Rubric", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "HealthySystem.Web.Controllers" });

            routes.MapRoute(
                name: "Tag",
                url: "t/{alias}",
                defaults: new { controller = "Tag", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "HealthySystem.Web.Controllers" });

            routes.MapRoute(
                name: "Article",
                url: "r/{rubric}/{alias}",
                defaults: new { controller = "Article", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "HealthySystem.Web.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "HealthySystem.Web.Controllers" });
        }
    }
}
