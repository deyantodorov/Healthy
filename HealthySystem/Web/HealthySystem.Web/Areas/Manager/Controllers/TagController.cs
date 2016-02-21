namespace HealthySystem.Web.Areas.Manager.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using HealthySystem.Common;
    using HealthySystem.Data.Models;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Web.Areas.Manager.ViewModels;
    using HealthySystem.Web.Areas.Manager.ViewModels.Tag;
    using HealthySystem.Web.Infrastructure.Mapping;

    public class TagController : AdministratorController
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page = 0)
        {
            var all = this.tagService.GetAll();

            this.ViewBag.CurrentSort = sortOrder;
            this.ViewBag.IdSortParam = string.IsNullOrEmpty(sortOrder) ? "IdAsc" : string.Empty;
            this.ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "NameDesc" : string.Empty;
            this.ViewBag.RubricSortParam = string.IsNullOrEmpty(sortOrder) ? "AliasDesc" : string.Empty;

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
                all = all.Where(t => t.Name.Contains(searchString.Trim()));
            }

            switch (sortOrder)
            {
                case "IdAsc":
                    all = all.OrderBy(s => s.Id);
                    break;
                case "NameDesc":
                    all = all.OrderByDescending(s => s.Name);
                    break;
                case "AliasDesc":
                    all = all.OrderByDescending(s => s.Alias);
                    break;
                default:
                    all = all.OrderByDescending(x => x.Id);
                    break;
            }

            int totalPages = (int)Math.Ceiling(all.Count() / (decimal)WebConstants.AdminPageSize);
            int pageNumber = page ?? 0;

            if (pageNumber > totalPages)
            {
                return this.NotFound("Page can't be found");
            }

            this.ViewBag.TotalPages = totalPages;
            this.ViewBag.CurrentPage = pageNumber;

            var allAsViewModel = all
                .Skip(pageNumber * WebConstants.AdminPageSize)
                .Take(WebConstants.AdminPageSize)
                .To<TagViewModel>()
                .ToList();

            return this.View(allAsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TagViewModel model)
        {
            if (this.tagService.AnyByName(model.Name, model.Alias))
            {
                return this.PartialView("_Error", new ErrorViewModel() { Message = ModelConstants.TagExist });
            }

            if (!this.ModelState.IsValid)
            {
                return this.PartialView("_Error", new ErrorViewModel() { Message = ModelConstants.ModelError });
            }

            var tag = this.Mapper.Map<Tag>(model);
            this.tagService.Add(tag);

            var newTag = this.Mapper.Map<TagViewModel>(tag);

            return this.PartialView("_ItemPartial", newTag);
        }

        [HttpPost]
        public ActionResult Edit(int id)
        {
            return this.PartialView("_UpdatePartial", this.GetTagById(id));
        }

        [HttpPost]
        public ActionResult Update(TagViewModel model)
        {
            if (this.tagService.AnyByNameId(model.Name, model.Alias, model.Id))
            {
                return this.PartialView("_Error", new ErrorViewModel() { Message = ModelConstants.TagExist });
            }

            if (!this.ModelState.IsValid)
            {
                return this.PartialView("_Error", new ErrorViewModel() { Message = ModelConstants.ModelError });
            }

            var tagForUpdate = this.Mapper.Map<Tag>(model);
            this.tagService.Update(tagForUpdate);

            return this.PartialView("_ItemPartial", model);
        }

        [HttpPost]
        public ActionResult Cancel(int id)
        {
            return this.PartialView("_ItemPartial", this.GetTagById(id));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var tag = this.tagService.GetById(id);

            if (tag != null && !tag.Articles.Any())
            {
                this.tagService.Delete(tag);
            }
            else
            {
                this.Session["Error"] = ModelConstants.Undeletable;
            }

            return this.RedirectToAction("Index", "Tag");
        }

        private TagViewModel GetTagById(int id)
        {
            var tag = this.tagService.GetById(id);

            if (tag == null)
            {
                this.ModelState.AddModelError(string.Empty, "Id " + id + " can't be found");
            }

            var tagViewModel = this.Mapper.Map<TagViewModel>(tag);
            return tagViewModel;
        }
    }
}