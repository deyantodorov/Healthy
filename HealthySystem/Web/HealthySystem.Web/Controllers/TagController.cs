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

    public class TagController : SiteController
    {
        private readonly ITagService tagService;
        private readonly IArticleService articleService;

        public TagController(ITagService tagService, IArticleService articleService)
        {
            this.tagService = tagService;
            this.articleService = articleService;
        }

        [HttpGet]
        public ActionResult Index(string alias, int? page)
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

            // TODO: Extract to service
            // Fake tag name return 404 error
            if (!this.tagService.GetAll().Any(x => x.Alias.ToLower().Equals(alias.ToLower())))
            {
                return new HttpNotFoundResult();
            }

            // TODO: Extract to service
            var articles = this.articleService.GetAll()
                .Where(x => x.IsPublished && x.PublishDate < DateTime.Now && x.Tags.Any(y => y.Alias == alias))
                .OrderByDescending(x => x.PublishDate)
                .Skip((pageSize - 1) * WebConstants.SiteRubricPageSize)
                .Take(WebConstants.SiteRubricPageSize)
                .To<ArticleSitePreviewViewModel>()
                .ToList();

            string tag = this.tagService.GetAll()
                .Where(x => x.Alias == alias)
                .ToList()[0]
                .Name;

            this.ViewBag.Tags = this.GetTags();
            this.ViewBag.Page = pageSize;
            this.ViewBag.Title = tag ?? string.Empty;

            articles.ForEach(x => x.Image = Images.GetImageFromCache(x.Image, WebConstants.ImageWidth, WebConstants.ImageMaxHeight));

            return this.View(articles);
        }
    }
}