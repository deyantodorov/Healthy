namespace HealthySystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using HealthySystem.Common;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Web.Infrastructure.Mapping;
    using HealthySystem.Web.ViewModels;

    public class TagController : SiteController
    {
        private readonly IArticleService articleService;

        public TagController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public ActionResult Index(string alias, int? page)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                return this.RedirectToActionPermanent("Index", "Home");
            }

            int pageSize;
            if (page != null)
            {
                pageSize = (int)page + 1;
            }
            else
            {
                pageSize = 1;
            }

            // Fake tag name return 404 error
            if (!this.TagService.GetAll().Any(x => x.Alias.ToLower().Equals(alias.ToLower())))
            {
                return new HttpNotFoundResult();
            }

            var articles = this.articleService.GetPublishedFromTagByAlias(alias, pageSize, WebConstants.SiteRubricPageSize)
                .To<ArticleSitePreviewViewModel>()
                .ToList();

            string tag = this.TagService.GetAll()
                .Where(x => x.Alias == alias)
                .ToList()[0]
                .Name;

            this.ViewBag.Tags = this.GetTags();
            this.ViewBag.Page = pageSize;
            this.ViewBag.Title = tag ?? string.Empty;

            return this.View(articles);
        }
    }
}