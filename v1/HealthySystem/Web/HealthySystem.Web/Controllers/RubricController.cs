namespace HealthySystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using HealthySystem.Common;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Web.Infrastructure.Mapping;
    using HealthySystem.Web.ViewModels;

    public class RubricController : SiteController
    {
        private readonly IRubricService rubricService;
        private readonly IArticleService articleService;

        public RubricController(IRubricService rubricService, IArticleService articleService)
        {
            this.rubricService = rubricService;
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

            // Get rubric info
            var rubric = this.rubricService.GetAll()
                .FirstOrDefault(x => x.Alias == alias);

            if (rubric == null)
            {
                return new HttpNotFoundResult();
            }

            // Get list of articles with current rubric
            var articles = this.articleService.GetPublishedFromRubricByAlias(alias, pageSize, WebConstants.SiteRubricPageSize)
                    .To<ArticleSitePreviewViewModel>()
                    .ToList();

            this.ViewBag.Tags = this.GetTags();
            this.ViewBag.Page = pageSize;
            this.ViewBag.Title = rubric.Title;
            this.ViewBag.Description = rubric.Description;
            this.ViewBag.PagingTitle = rubric.Name.ToLower();

            return this.View(articles);
        }
    }
}