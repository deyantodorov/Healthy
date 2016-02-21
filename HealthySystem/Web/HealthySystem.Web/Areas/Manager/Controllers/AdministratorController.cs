namespace HealthySystem.Web.Areas.Manager.Controllers
{
    using System.Web.Mvc;
    using HealthySystem.Services.Web.Contracts;
    using HealthySystem.Web.Controllers;

    // TODO: RESTRICT TO ADMINISTRATOR OR EDITOR
    [Authorize]
    public class AdministratorController : BaseController
    {
    }
}