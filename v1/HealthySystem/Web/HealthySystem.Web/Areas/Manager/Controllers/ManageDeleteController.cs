namespace HealthySystem.Web.Areas.Manager.Controllers
{
    using System.Web.Mvc;

    [Authorize(Roles = "Administrator")]
    public class ManageDeleteController : AdministratorController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}