namespace HealthySystem.Web.Controllers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class TagControllerTests : BaseControllerTests
    {
        private TagController controller;

        [TestInitialize]
        public void Setup()
        {
            this.controller = new TagController(this.articleServiceMock.Object)
            {
                Cache = this.cacheServiceMock.Object
            };
        }

        [TestMethod]
        public void ShouldRedirectToRoot()
        {
            this.controller.WithCallTo(x => x.Index(string.Empty, null))
                .ShouldRedirectToRoute(string.Empty);
        }
    }
}
