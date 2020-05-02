namespace HealthySystem.Web.Areas.Manager.Controllers
{
    using System.Web.Mvc;
    using HealthySystem.Services.Web.Contracts;

    public class HomeController : AdministratorController
    {
        private readonly ITransliterator transliterator;

        public HomeController(ITransliterator transliterator)
        {
            this.transliterator = transliterator;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Transliterate(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return this.Content(string.Empty);
            }

            var result = this.transliterator.GetTextInEnglish(content);
            return this.Content(result);
        }
    }
}