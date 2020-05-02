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
            this.controller = new SearchController(this.ArticleServiceMock.Object, this.SecuritySanitizer.Object)
            {
                Cache = this.CacheServiceMock.Object
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