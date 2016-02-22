namespace HealthySystem.Web.Areas.Manager.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using HealthySystem.Common;
    using HealthySystem.Data.Models;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Web.Areas.Manager.ViewModels.Rubric;
    using HealthySystem.Web.Infrastructure.Mapping;

    public class RubricController : AdministratorController
    {
        private readonly IRubricService rubricService;
        private readonly IArticleService articleService;

        public RubricController(IRubricService rubricService, IArticleService articleService)
        {
            this.rubricService = rubricService;
            this.articleService = articleService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (this.TempData.ContainsKey(ModelConstants.Error))
            {
                this.ViewBag.Error = this.TempData[ModelConstants.Error];
            }

            var rubrics = this.rubricService.GetAll().To<RubricViewModel>().ToList();
            return this.View(rubrics);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(RubricViewModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return new HttpStatusCodeResult(400, ModelConstants.ModelError);
            }

            var rubric = this.Mapper.Map<Rubric>(model);

            if (!this.IsCorrectRubric(rubric) || !this.TryFindRubricByParentId(rubric))
            {
                return new HttpStatusCodeResult(400, ModelConstants.ModelError);
            }

            this.rubricService.Add(rubric);

            var newRubricModel = this.Mapper.Map<RubricViewModel>(rubric);

            return this.PartialView("_ItemPartial", newRubricModel);
        }

        [HttpPost]
        public ActionResult Edit(int id)
        {
            return this.PartialView("_EditPartial", this.GetRubricById(id));
        }

        [HttpPost]
        public ActionResult Update(RubricViewModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return new HttpStatusCodeResult(400, ModelConstants.ModelError);
            }

            var forUpdate = this.Mapper.Map<Rubric>(model);
            if (!this.IsCorrectRubric(forUpdate) || !this.TryFindRubricByParentId(forUpdate))
            {
                return new HttpStatusCodeResult(400, ModelConstants.ModelError);
            }

            this.rubricService.Update(forUpdate);
            return this.PartialView("_ItemPartial", model);
        }

        [HttpPost]
        public ActionResult UpdateCancel(int id)
        {
            return this.PartialView("_ItemPartial", this.GetRubricById(id));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var rubric = this.rubricService.GetById(id);

            if (rubric == null)
            {
                return new HttpStatusCodeResult(400, ModelConstants.NotFound);
            }

            if (this.rubricService.IsParent(id) || this.articleService.HasRubricId(id))
            {
                return new HttpStatusCodeResult(400, ModelConstants.Undeletable);
            }

            this.rubricService.Delete(rubric);

            return this.RedirectToAction("Index", "Rubric");
        }

        private RubricViewModel GetRubricById(int id)
        {
            var getRubricById = this.rubricService.GetById(id);

            if (getRubricById == null)
            {
                this.NotFound(ModelConstants.NotFound);
            }

            var rubric = this.Mapper.Map<RubricViewModel>(getRubricById);
            return rubric;
        }

        private bool IsCorrectRubric(Rubric rubric)
        {
            var tryFind = this.rubricService
                .GetAll()
                .Where(r => r.Name == rubric.Name
                        && r.ParentId == rubric.ParentId
                        && r.Alias == rubric.Alias)
                .ToList();

            return !tryFind.Any() && rubric.Id != rubric.ParentId;
        }

        private bool TryFindRubricByParentId(Rubric rubric)
        {
            var tryFind = this.rubricService.GetById(rubric.ParentId ?? 0);
            return tryFind != null || rubric.ParentId == null;
        }
    }
}