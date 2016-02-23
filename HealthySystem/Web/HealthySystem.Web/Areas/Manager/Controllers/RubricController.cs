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

            if (this.IncorrectRubric(rubric))
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

            if (this.IncorrectRubric(forUpdate))
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

        private bool IncorrectRubric(Rubric rubric)
        {
            bool isParentRubricExist = false;

            if (rubric.ParentId != null)
            {
                isParentRubricExist = !this.rubricService.GetAll().Any(x => x.Id != rubric.Id && x.Id == rubric.ParentId);
            }

            bool isParentRubricIdIsMyId = rubric.Id == rubric.ParentId;

            return isParentRubricExist || isParentRubricIdIsMyId;
        }
    }
}