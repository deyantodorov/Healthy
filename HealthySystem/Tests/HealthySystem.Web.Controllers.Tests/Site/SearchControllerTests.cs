namespace HealthySystem.Web.Controllers.Tests.Site
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class SearchControllerTests : BaseControllerTests
    {
        private SearchController controller;

        [TestInitialize]
        public void Setup()
        {
            this.controller = new SearchController(this.articleServiceMock.Object, this.securitySanitizer.Object)
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