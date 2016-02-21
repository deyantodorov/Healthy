namespace HealthySystem.Web.Areas.Manager.Controllers
{
    using System.Web.Mvc;
    using HealthySystem.Web.Controllers;

    // TODO: RESTRICT TO ADMINISTRATOR OR EDITOR
    [Authorize]
    public class AdministratorController : BaseController
    {
    }
}