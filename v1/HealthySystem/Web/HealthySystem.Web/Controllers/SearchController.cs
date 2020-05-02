namespace HealthySystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using HealthySystem.Common;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Services.Web.Contracts;
    using HealthySystem.Web.Infrastructure.Mapping;
    using HealthySystem.Web.ViewModels;

    public class SearchController : SiteController
    {
        private readonly IArticleService articleService;
        private readonly IHtmlSecuritySanitizer htmlSecuritySanitizer;

        public SearchController(IArticleService articleService, IHtmlSecuritySanitizer htmlSecuritySanitizer)
        {
            this.articleService = articleService;
            this.htmlSecuritySanitizer = htmlSecuritySanitizer;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Index(string search, int? page)
        {
            if (string.IsNullOrEmpty(search))
            {
                return this.RedirectToActionPermanent("Index", "Home");
            }

            search = this.htmlSecuritySanitizer.Clean(search);

            int pageSize;
            if (page != null)
            {
                pageSize = (int)page + 1;
            }
            else
            {
                pageSize = 1;
            }

            // Get list of articles with current rubric
            var articles = this.articleService.GetAll()
                    .Where(x => x.IsPublished && x.PublishDate <= DateTime.Now && x.Title.Contains(search.Trim()))
                    .OrderByDescending(x => x.PublishDate)
                    .Take(WebConstants.SiteSearchPageSize)
                    .To<ArticleSitePreviewViewModel>()
                    .ToList();

            this.ViewBag.Tags = this.GetTags();
            this.ViewBag.Page = pageSize;
            this.ViewBag.Title = search.Trim();

            return this.View(articles);
        }
    }
}