namespace HealthySystem.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using HealthySystem.Services.Web.Contracts;
    using HealthySystem.Web.Infrastructure.Mapping;

    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }

        protected IMapper Mapper => AutoMapperConfig.Configuration.CreateMapper();

        protected HttpNotFoundResult NotFound(string message)
        {
            return this.HttpNotFound(message);
        }
    }
}