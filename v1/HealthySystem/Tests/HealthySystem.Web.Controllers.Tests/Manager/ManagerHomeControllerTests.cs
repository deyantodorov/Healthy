namespace HealthySystem.Web.Controllers.Tests.Manager
{
    using HealthySystem.Web.Areas.Manager.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class ManagerHomeControllerTests : BaseControllerTests
    {
        private HomeController controller;

        [TestInitialize]
        public void Setup()
        {
            this.controller = new HomeController(this.Transliterator.Object);
        }

        [TestMethod]
        public void ShouldReturnIndexAsDefault()
        {
            this.controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ShouldReturnStringFromTransliterator()
        {
            this.controller.WithCallTo(x => x.Transliterate("asdf"))
                .ShouldReturnContent();
        }
    }
}
