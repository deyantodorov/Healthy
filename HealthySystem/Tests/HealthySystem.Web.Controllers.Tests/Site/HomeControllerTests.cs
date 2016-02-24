namespace HealthySystem.Web.Controllers.Tests.Site
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class HomeControllerTests : BaseControllerTests
    {
        private HomeController controller;

        [TestInitialize]
        public void Setup()
        {
            this.controller = new HomeController(this.ArticleServiceMock.Object)
            {
                Cache = this.CacheServiceMock.Object
            };
        }

        [TestMethod]
        public void TestHomeControllerShouldRenderDefaultIndex()
        {
            this.controller.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void TestHomeControllerIndex()
        {
            this.controller.WithCallTo(x => x.Index())
                .ShouldRenderView("Index");
        }

        [TestMethod]
        public void TestHomeControllerAll()
        {
            this.controller.WithCallTo(x => x.All())
                .ShouldRenderView("All");
        }
    }
}