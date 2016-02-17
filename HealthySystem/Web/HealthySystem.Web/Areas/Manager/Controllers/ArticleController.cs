namespace HealthySystem.Web.Areas.Manager.Controllers
{
    using System.Web.Mvc;

    public class ArticleController : AdministratorController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}