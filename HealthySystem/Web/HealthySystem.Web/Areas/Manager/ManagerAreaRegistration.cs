namespace HealthySystem.Web.Areas.Manager
{
    using System.Web.Mvc;

    public class ManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Manager";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Manager_Default",
                "Manager/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "HealthySystem.Web.Areas.Manager.Controllers" });
        }
    }
}