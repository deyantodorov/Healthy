namespace HealthySystem.Web
{
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using HealthySystem.Web.Infrastructure.Mapping;

    #pragma warning disable SA1649 // File name must match first type name
    public class MvcApplication : HttpApplication
    #pragma warning restore SA1649 // File name must match first type name
    {
        protected void Application_Start()
        {
            // ViewEngines, Db Migrations, DI - AutoFac
            ViewEngineConfig.Initialize();
            DatabaseConfig.Initialize();
            AutofacConfig.RegisterAutofac();

            // MVC stuff
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // AutoMapper configuration
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(Assembly.GetExecutingAssembly());
        }
    }
}