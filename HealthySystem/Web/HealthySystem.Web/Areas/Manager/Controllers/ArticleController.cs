namespace HealthySystem.Web.Areas.Manager.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using HealthySystem.Common;
    using HealthySystem.Data.Models;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Web.Areas.Manager.ViewModels.Article;
    using HealthySystem.Web.Areas.Manager.ViewModels.Rubric;
    using HealthySystem.Web.Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Infrastructure.Images;

    public class ArticleController : AdministratorController
    {
        private readonly IArticleService articleService;
        private readonly IRubricService rubricService;
        private readonly IImageService imageService;
        private readonly ITagService tagService;

        public ArticleController(IArticleService articleService, IRubricService rubricService, IImageService imageService, ITagService tagService)
        {
            this.articleService = articleService;
            this.rubricService = rubricService;
            this.imageService = imageService;
            this.tagService = tagService;
        }

        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page = 0)
        {
            var all = this.articleService.GetAll();
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

        [HttpGet]
        public ActionResult Add()
        {
            var urlReferrer = this.HttpContext.Request.UrlReferrer;
            var articleViewModel = new ArticleViewModel();

            if (urlReferrer != null)
            {
                articleViewModel.ReferrerUrl = urlReferrer.AbsoluteUri;
            }

            articleViewModel.CreatedOn = DateTime.Now;
            articleViewModel.Rubrics = this.GetRubricsAsCollectionOfListItems();
            articleViewModel.AuthorId = this.User.Identity.Name;

            return this.View(articleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ArticleViewModel model)
        {
            if (this.articleService.AnyByTitle(model.Title))
            {
                this.ModelState.AddModelError("Title", ModelConstants.TitleExist);

                var currentImage = this.imageService.GetById(model.ImageId ?? 0);

                if (currentImage != null)
                {
                    this.ViewBag.ImagePath = currentImage.ImagePath;
                    this.ViewBag.ImageDesc = currentImage.ImageDescription;
                }

                model.Rubrics = this.GetRubricsAsCollectionOfListItems(model.RubricId);
                return this.View(model);
            }

            if (this.ModelState.IsValid)
            {
                Article article = this.MakeArtilceFromModel(model);
                article.Tags = this.GetTags(model.Tags);

                this.articleService.Add(article);

                if (model.ReferrerUrl != null)
                {
                    return this.Redirect(model.ReferrerUrl);
                }

                return this.RedirectToAction("Index");
            }

            model.Rubrics = this.GetRubricsAsCollectionOfListItems(model.RubricId);

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var article = this.articleService.GetById(id);

            if (article == null)
            {
                this.NotFound(string.Format("Article with {0} can't be found", id));
            }

            var articleViewModel = this.Mapper.Map<ArticleViewModel>(article);
            articleViewModel.Rubrics = this.GetRubricsAsCollectionOfListItems(articleViewModel.RubricId);

            var currentImage = this.imageService.GetById(articleViewModel.ImageId ?? 0);

            if (currentImage != null)
            {
                this.ViewBag.ImagePath = Images.GetImageFromCache(currentImage.ImagePath, WebConstants.ImageWidth, WebConstants.ImageMaxHeight);
                this.ViewBag.ImageDesc = currentImage.ImageDescription;
            }

            var urlReferrer = this.HttpContext.Request.UrlReferrer;

            if (urlReferrer != null)
            {
                articleViewModel.ReferrerUrl = urlReferrer.AbsoluteUri;
            }

            return this.View(articleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ArticleViewModel model)
        {
            if (this.articleService.AnyByTitleAndId(model.Title, model.Id))
            {
                this.ModelState.AddModelError("Title", "Заглавието вече съществува");
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Update", "Article", model);
            }

            var articleFromDb = this.articleService.GetById(model.Id);

            articleFromDb.Tags.Clear();
            this.articleService.Update(articleFromDb);

            articleFromDb.Tags = this.GetTags(model.Tags);
            articleFromDb.Title = model.Title;
            articleFromDb.Description = model.Description;
            articleFromDb.Content = model.Content;
            articleFromDb.IsPublished = model.IsPublished;
            articleFromDb.PublishDate = DateTime.Parse(model.PublishDate.ToString());
            articleFromDb.UnpublishDate = model.UnpublishDate;
            articleFromDb.ImageId = model.ImageId;
            articleFromDb.Alias = model.Alias;
            articleFromDb.RubricId = model.RubricId;
            articleFromDb.AuthorId = this.User.Identity.GetUserId();

            this.articleService.Update(articleFromDb);

            if (model.ReferrerUrl != null)
            {
                return this.Redirect(model.ReferrerUrl);
            }

            return this.RedirectToAction("Index", "Article");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var article = this.articleService.GetById(id);

            if (article == null)
            {
                this.NotFound(string.Format("Article with {0} can't be found", id));
            }

            this.articleService.Delete(article);

            return this.RedirectToAction("Index", "Article");
        }

        [HttpPost]
        public JsonResult SearchForTag(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            var foundTags = this.tagService
                .GetAll()
                .Where(x => x.Name.ToLower().Contains(content.Trim().ToLower()))
                .Select(x => new
                {
                    Name = x.Name,
                })
                .ToList();

            return this.Json(foundTags);
        }

        [HttpPost]
        public JsonResult SearchForImage(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            var found = this.imageService
                .GetAll()
                .Where(x => x.ImageDescription.ToLower().Contains(content.Trim().ToLower()))
                .Select(x => new
                {
                    Id = x.Id,
                    Path = x.ImagePath,
                    Desc = x.ImageDescription
                })
                .ToList();

            // TODO: Didn't find better way may improve it in advance
            // set image cache source
            var resultAsCachedImages = found.Select(image => new
            {
                image.Id,
                Path = Images.GetImageFromCache(image.Path, WebConstants.ImageWidth, WebConstants.ImageMaxHeight),
                image.Desc
            })
            .Cast<object>()
            .ToList();

            return this.Json(resultAsCachedImages);
        }

        private IEnumerable<SelectListItem> GetRubricsAsCollectionOfListItems(int id = 0)
        {
            var rubricViewModels = this.Cache.Get(
                    WebConstants.CacheRubricFromDb,
                    () => this.rubricService.GetAll().To<RubricViewModel>().ToList(),
                    WebConstants.AdminCacheTime);

            var result = new List<RubricViewModel>();

            foreach (var rubric in rubricViewModels)
            {
                if (rubric.ParentId != null)
                {
                    var current = new RubricViewModel
                    {
                        Id = rubric.Id,
                        Name = this.GetParentName(rubric.ParentId, rubricViewModels) + " / " + rubric.Name,
                        Alias = rubric.Alias,
                        ParentId = rubric.ParentId
                    };

                    result.Add(current);
                }
                else
                {
                    result.Add(rubric);
                }
            }

            var list = result.Select(
                r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name,
                    Selected = r.Id == id,
                })
                .OrderBy(r => r.Text);

            return list;
        }

        private string GetParentName(int? parentId, IEnumerable<RubricViewModel> rubricViewModels)
        {
            foreach (var rubric in rubricViewModels)
            {
                if (rubric.Id == parentId)
                {
                    return rubric.Name;
                }
            }

            return string.Empty;
        }

        private Article MakeArtilceFromModel(ArticleViewModel model)
        {
            var article = new Article
            {
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                IsPublished = model.IsPublished,
                CreatedOn = model.CreatedOn,
                PublishDate = DateTime.Parse(model.PublishDate.ToString()),
                UnpublishDate = model.UnpublishDate,
                ImageId = model.ImageId,
                Alias = model.Alias,
                RubricId = model.RubricId,
                AuthorId = this.User.Identity.GetUserId(),
                Tags = model.Tags == null ? null : this.GetTags(model.Tags),
            };

            return article;
        }

        private ICollection<Tag> GetTags(string tags)
        {
            var allTags = this.FilterTags(tags);

            ICollection<Tag> foundTags = new HashSet<Tag>();

            foreach (var current in allTags.Select(item => this.tagService.GetByName(item)).Where(current => current != null))
            {
                // TODO: Need decrease Rank to be implemented when delete article tags
                current.Rank++;
                foundTags.Add(current);
            }

            return foundTags;
        }

        private IEnumerable<string> FilterTags(string tags)
        {
            var allTags = tags.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var filteredTags = new HashSet<string>();

            foreach (var tag in allTags)
            {
                filteredTags.Add(tag.Trim());
            }

            return filteredTags;
        }
    }
}