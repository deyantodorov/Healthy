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

            // Fake rubric name return 404 error
            if (!this.rubricService.GetAll().Any(x => x.Alias.ToLower().Equals(alias.ToLower())))
            {
                return new HttpNotFoundResult();
            }

            // Get list of articles with current rubric
            var articles = this.articleService.GetPublishedFromRubricByAlias(alias, pageSize, WebConstants.SiteRubricPageSize)
                    .To<ArticleSitePreviewViewModel>()
                    .ToList();

            this.ViewBag.Tags = this.GetTags();
            this.ViewBag.Page = pageSize;

            // Get rubric title
            string title = this.rubricService.GetAll()
                .Where(x => x.Alias == alias)
                .Select(x => x.Name)
                .Single();

            this.ViewBag.Title = string.Empty;
            this.ViewBag.Description = string.Empty;
            this.ViewBag.PagingTitle = title;

            switch (title.ToLower())
            {
                case "лечение":
                    this.ViewBag.Title = WebConstants.LechenieTitle;
                    this.ViewBag.Description = WebConstants.LechenieDesc;
                    break;
                case "здраве":
                    this.ViewBag.Title = WebConstants.ZdraveTitle;
                    this.ViewBag.Description = WebConstants.ZdraveDesc;
                    break;
                case "психология":
                    this.ViewBag.Title = WebConstants.PsihologiyaTitle;
                    this.ViewBag.Description = WebConstants.PsihologiyaDesc;
                    break;
                case "здравословно хранене":
                    this.ViewBag.Title = WebConstants.ZdravoslovnoHraneneTitle;
                    this.ViewBag.Description = WebConstants.ZdravoslovnoHraneneDesc;
                    break;
                case "секс и здраве":
                    this.ViewBag.Title = WebConstants.SeksZdraveTitle;
                    this.ViewBag.Description = WebConstants.SeksZdraveDesc;
                    break;
                case "деца":
                    this.ViewBag.Title = WebConstants.DetsaTitle;
                    this.ViewBag.Description = WebConstants.DetsaDesc;
                    break;
                case "красота":
                    this.ViewBag.Title = WebConstants.KrasotaTitle;
                    this.ViewBag.Description = WebConstants.KrasotaDesc;
                    break;
            }

            return this.View(articles);
        }
    }
}