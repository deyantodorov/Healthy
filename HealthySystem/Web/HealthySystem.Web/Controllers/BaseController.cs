namespace HealthySystem.Web.Controllers
{
    using System.Web.Mvc;
    using HealthySystem.Services.Web.Contracts;

    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }
    }
}