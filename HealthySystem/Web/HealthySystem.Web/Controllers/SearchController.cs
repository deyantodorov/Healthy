namespace HealthySystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using HealthySystem.Common;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Web.Infrastructure.Images;
    using HealthySystem.Web.Infrastructure.Mapping;
    using HealthySystem.Web.ViewModels;

    public class SearchController : SiteController
    {
        private readonly IArticleService articleService;

        public SearchController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        // TODO: Sanitize it
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string search, int? page)
        {
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

            articles.ForEach(x => x.Image = Images.GetImageFromCache(x.Image, WebConstants.ImageWidth, WebConstants.ImageMaxHeight));

            return this.View(articles);
        }
    }
}