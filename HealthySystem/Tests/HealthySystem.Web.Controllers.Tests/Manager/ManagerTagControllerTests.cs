namespace HealthySystem.Web.Controllers.Tests.Manager
{
    using HealthySystem.Web.Areas.Manager.Controllers;
    using HealthySystem.Web.Areas.Manager.ViewModels.Tag;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class ManagerTagControllerTests : BaseControllerTests
    {
        private TagController controller;
        private TagViewModel model;

        [TestInitialize]
        public void Setup()
        {
            this.model = new TagViewModel() { Alias = "tag", Name = "tag", Rank = 0 };
            this.controller = new TagController(this.TagServiceMock.Object);
        }

        [TestMethod]
        public void PostAddShouldWorkOk()
        {
            this.controller.WithCallTo(x => x.Add(this.model))
                .ShouldRenderPartialView("_ItemPartial");
        }

        [TestMethod]
        public void EditByIdShoulBeOk()
        {
            this.controller.WithCallTo(x => x.Edit(1))
                .ShouldRenderPartialView("_UpdatePartial");
        }

        [TestMethod]
        public void UpdateShoulBeOk()
        {
            this.controller.WithCallTo(x => x.Update(this.model))
                .ShouldRenderPartialView("_ItemPartial");
        }

        [TestMethod]
        public void CancelShoulBeOk()
        {
            this.controller.WithCallTo(x => x.Cancel(1))
                .ShouldRenderPartialView("_ItemPartial");
        }

        [TestMethod]
        public void DeleteShoulNotBeOk()
        {
            this.controller.WithCallTo(x => x.Delete(-1))
                .ShouldGiveHttpStatus(400);
        }
    }
}