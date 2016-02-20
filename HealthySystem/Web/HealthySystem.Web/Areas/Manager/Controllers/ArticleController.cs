namespace HealthySystem.Web.Areas.Manager.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using HealthySystem.Common;
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

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var all = this.articles.GetAll();
            this.ViewBag.CurrentSort = sortOrder;
            this.ViewBag.IdSortParam = string.IsNullOrEmpty(sortOrder) ? "IdAsc" : string.Empty;
            this.ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "TitleDesc" : string.Empty;
            this.ViewBag.RubricSortParam = string.IsNullOrEmpty(sortOrder) ? "RubricDesc" : string.Empty;

            if (searchString != null)
            {
                page = 0;
            }
            else
            {
                searchString = currentFilter;
            }

            this.ViewBag.CurrentFilter = searchString;

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower().Trim();
                all = all.Where(t => t.Title.Contains(searchString.Trim()));
            }

            switch (sortOrder)
            {
                case "IdAsc":
                    all = all.OrderBy(s => s.Id);
                    break;
                case "TitleDesc":
                    all = all.OrderByDescending(s => s.Title);
                    break;
                case "RubricDesc":
                    all = all.OrderByDescending(s => s.Rubric.Name);
                    break;
                default:
                    all = all.OrderByDescending(x => x.Id);
                    break;
            }

            int totalPages = (int)Math.Ceiling(all.Count() / (decimal)WebConstants.AdminPageSize);
            int pageNumber = page ?? 0;

            if (pageNumber > totalPages)
            {
                return new HttpNotFoundResult("Page can't be found");
            }

            this.ViewBag.TotalPages = totalPages;
            this.ViewBag.CurrentPage = pageNumber;

            var allAsViewModel = all
                .Skip(pageNumber * WebConstants.AdminPageSize)
                .Take(WebConstants.AdminPageSize)
                .To<ArticlePreviewViewModel>()
                .ToList();

            return this.View(allAsViewModel);
        }
    }
}