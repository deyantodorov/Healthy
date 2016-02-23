namespace HealthySystem.Web.Controllers.Tests.Site
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class RubricControllerTests : BaseControllerTests
    {
        private RubricController controller;

        [TestInitialize]
        public void Setup()
        {
            this.controller = new RubricController(this.rubricServiceMock.Object, this.articleServiceMock.Object)
            {
                Cache = this.cacheServiceMock.Object
            };
        }

        [TestMethod]
        public void ShouldRenderDefaultIndext()
        {
            this.controller.WithCallTo(x => x.Index("rubrica-1", null))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ShouldRedirectToRoot()
        {
            this.controller.WithCallTo(x => x.Index(string.Empty, null))
                .ShouldRedirectToRoute(string.Empty);
        }

        [TestMethod]
        public void ShouldReturnNotFound()
        {
            this.controller.WithCallTo(x => x.Index("asdf", null))
                .ShouldGiveHttpStatus(404);
        }
    }
}