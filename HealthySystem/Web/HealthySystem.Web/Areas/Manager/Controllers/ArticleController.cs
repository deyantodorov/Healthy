namespace HealthySystem.Web.Areas.Manager.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Web.Areas.Manager.ViewModels.Article;
    using HealthySystem.Web.Infrastructure.Mapping;

    public class ArticleController : AdministratorController
    {
        private readonly IArticleService articles;

        public ArticleController(IArticleService articles)
        {
            this.articles = articles;
        }


        public ActionResult Index()
        {
            var articlesAsModel = this.articles.GetAll().To<ArticlePreviewViewModel>().ToList();
            return this.View(articlesAsModel);
        }
    }
}