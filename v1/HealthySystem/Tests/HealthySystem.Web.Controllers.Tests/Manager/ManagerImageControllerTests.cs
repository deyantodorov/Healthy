namespace HealthySystem.Web.Controllers.Tests.Manager
{
    using System.Collections.Generic;
    using System.Web;
    using HealthySystem.Web.Areas.Manager.Controllers;
    using HealthySystem.Web.Areas.Manager.ViewModels.Image;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class ManagerImageControllerTests : BaseControllerTests
    {
        private ImageController controller;
        private ImageViewModel goodImageViewModel;
        private ImageViewModel badImageViewModel;

        [TestInitialize]
        public void Setup()
        {
            this.badImageViewModel = new ImageViewModel()
            {
                Id = 1,
                ImagePath = null,
                ImageDescription = "Description",
            };

            this.goodImageViewModel = new ImageViewModel()
            {
                Id = 1,
                ImagePath = new MemoryFile(),
                ImageDescription = "Description",
            };

            this.controller = new ImageController(this.ImageServiceMock.Object, this.ArticleServiceMock.Object);
        }

        [TestMethod]
        public void Image_Should_Return_Default()
        {
            this.controller.WithCallTo(x => x.Index(null, null, null, null))
                .ShouldRenderDefaultView()
                .WithModel<List<ImagePreviewViewModel>>();
        }

        [TestMethod]
        public void Add_Image_With_Null_Should_Redirect_To_Index()
        {
            this.controller.WithCallTo(x => x.Add(null))
                .ShouldRedirectToRoute(string.Empty);
        }

        [TestMethod]
        public void Delete_Image_Should_Return_NotFound()
        {
            this.controller.WithCallTo(x => x.Delete(-1))
                .ShouldGiveHttpStatus(404);
        }

        [TestMethod]
        public void Delete_Should_Return_Status_404()
        {
            this.controller.WithCallTo(x => x.Delete(-1))
                .ShouldGiveHttpStatus(404);
        }

        [TestMethod]
        public void Delete_Should_Return_Ok()
        {
            this.controller.WithCallTo(x => x.Add(null))
                .ShouldRedirectToRoute(string.Empty);
        }

        [TestMethod]
        public void Edit_Should_Return_Ok()
        {
            this.controller.WithCallTo(x => x.Edit(1))
                .ShouldRenderPartialView("_EditPartial");
        }

        [TestMethod]
        public void Update_With_Null_Should_Return_Error()
        {
            this.controller.WithCallTo(x => x.Update(null))
                .ShouldRenderPartialView("_Error");
        }

        [TestMethod]
        public void Update_Will_Be_Ok()
        {
            var model = new ImagePreviewViewModel()
            {
                Id = 1,
                ImageDescription = "asdfasdf",
                ImagePath = "asdf.asdf"
            };

            this.controller.WithCallTo(x => x.Update(model))
                .ShouldRenderPartialView("_ItemPartial");
        }

        [TestMethod]
        public void Cancel_Will_Be_Ok()
        {
            this.controller.WithCallTo(x => x.Cancel(1))
                .ShouldRenderPartialView("_ItemPartial");
        }
    }
}