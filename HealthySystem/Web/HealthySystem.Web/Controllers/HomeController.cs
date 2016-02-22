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

    public class HomeController : SiteController
    {
        private readonly IArticleService articleService;

        public HomeController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var articles = this.Cache.Get(
                WebConstants.SiteCacheArticlesHome,
                () => this.articleService.GetAll()
                .Where(x => x.IsPublished && x.PublishDate <= DateTime.Now)
                .OrderByDescending(x => x.PublishDate)
                .Take(WebConstants.SiteHomePageSize)
                .To<ArticleSitePreviewViewModel>()
                .ToList(),
                WebConstants.Min5);

            articles.ForEach(x => x.Image = Images.GetImageFromCache(x.Image, WebConstants.ImageWidth, WebConstants.ImageMaxHeight));

            this.ViewBag.Tags = this.GetTags();

            return this.View(articles);
        }

        [HttpGet]
        public ActionResult All()
        {
            var articles = this.Cache.Get(
                WebConstants.SiteCacheArticlesSiteMap,
                () => this.articleService.GetAll()
                .Where(x => x.IsPublished && x.PublishDate <= DateTime.Now)
                .OrderByDescending(x => x.PublishDate)
                .To<ArticleSiteShortPreviewViewModel>()
                .ToList(),
                WebConstants.Min60);

            return this.View(articles);
        }
    }
}