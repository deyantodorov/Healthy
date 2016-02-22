namespace HealthySystem.Web.Areas.Manager.Controllers
{
    using System.Web.Mvc;
    using HealthySystem.Web.Controllers;

    [Authorize(Roles = "Administrator, Editor")]
    public class AdministratorController : BaseController
    {
    }
}