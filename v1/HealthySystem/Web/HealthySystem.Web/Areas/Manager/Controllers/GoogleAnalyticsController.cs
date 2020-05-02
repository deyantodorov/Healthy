namespace HealthySystem.Web.Areas.Manager.Controllers
{
    using System.Web.Mvc;

    [Authorize(Roles = "Administrator")]
    public class GoogleAnalyticsController : AdministratorController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}