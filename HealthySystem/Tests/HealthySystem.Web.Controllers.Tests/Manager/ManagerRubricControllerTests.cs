namespace HealthySystem.Web.Controllers.Tests.Manager
{
    using System.Collections.Generic;
    using HealthySystem.Web.Areas.Manager.Controllers;
    using HealthySystem.Web.Areas.Manager.ViewModels.Rubric;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class ManagerRubricControllerTests : BaseControllerTests
    {
        private RubricController controller;
        private RubricViewModel goodRubric;
        private RubricViewModel badRubric;

        [TestInitialize]
        public void Setup()
        {
            this.badRubric= new RubricViewModel()
            {
                Alias = "alias",
                Description = "alias",
                Id = 1,
                Name = "name",
                ParentId = 1,
                Title = "title"
            };

            this.goodRubric= new RubricViewModel()
            {
                Alias = "alias",
                Description = "alias",
                Id = 1,
                Name = "name",
                ParentId = 2,
                Title = "title"
            };

            this.controller = new RubricController(this.rubricServiceMock.Object, this.articleServiceMock.Object);
        }

        [TestMethod]
        public void ShouldReturnDefault()
        {
            this.controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<RubricViewModel>>();
        }

        [TestMethod]
        public void AddWithNullShouldRespondWithStatus400()
        {
            this.controller.WithCallTo(x => x.Add(null))
                .ShouldGiveHttpStatus(400);
        }

        [TestMethod]
        public void Add_Rubric_To_Itself_Should_Respond_With_Status_400()
        {
            this.controller.WithCallTo(x => x.Add(this.badRubric))
                .ShouldGiveHttpStatus(400);
        }

        [TestMethod]
        public void Add_Rubric_Should_Be_Ok()
        {
            this.controller.WithCallTo(x => x.Add(this.goodRubric))
                .ShouldRenderPartialView("_ItemPartial");
        }

        [TestMethod]
        public void Edit_Should_Return_Ok()
        {
            this.controller.WithCallTo(x => x.Edit(1))
                .ShouldRenderPartialView("_EditPartial");
        }

        [TestMethod]
        public void Update_With_Null_Should_Return_400()
        {
            this.controller.WithCallTo(x => x.Update(null))
                .ShouldGiveHttpStatus(400);
        }

        [TestMethod]
        public void Update_With_Self_Should_Return_400()
        {
            this.controller.WithCallTo(x => x.Update(this.badRubric))
                .ShouldGiveHttpStatus(400);
        }

        [TestMethod]
        public void Update_Will_Be_Ok()
        {
            this.controller.WithCallTo(x => x.Update(this.goodRubric))
                .ShouldRenderPartialView("_ItemPartial");
        }

        [TestMethod]
        public void Update_Cancel_Will_Be_Ok()
        {
            this.controller.WithCallTo(x => x.Cancel(1))
                .ShouldRenderPartialView("_ItemPartial");
        }
    }
}