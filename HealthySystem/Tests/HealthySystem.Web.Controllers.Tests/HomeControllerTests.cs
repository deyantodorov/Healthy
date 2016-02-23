namespace HealthySystem.Web.Controllers.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Services.Web.Contracts;
    using HealthySystem.Web.Controllers.Tests.FakeDbRepository;
    using HealthySystem.Web.Infrastructure.Mapping;
    using HealthySystem.Web.ViewModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class HomeControllerTests
    {
        private HomeController controller;
        private Mock<IArticleService> articleServiceMock;
        private Mock<ICacheService> cacheServiceMock;
        private AutoMapperConfig autoMapperConfig;

        [TestInitialize]
        public void Init()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            this.autoMapperConfig.Execute(typeof(HomeController).Assembly);

            this.cacheServiceMock = new Mock<ICacheService>();
            this.cacheServiceMock.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<object>, It.IsAny<int>()))
                .Returns(ArticleDbRepository.GetArticles());

            this.articleServiceMock = new Mock<IArticleService>();
            this.articleServiceMock.Setup(x => x.AnyByTitle(It.IsAny<string>())).Returns(false);

            this.controller = new HomeController(this.articleServiceMock.Object)
            {
                Cache = this.cacheServiceMock.Object
            };
        }

        [TestMethod]
        public void TestHomeControllerShouldRenderDefaultIndex()
        {
            this.controller.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }

        // [TestMethod]
        // public void TestHomeControllerIndexShouldMapDataCorrectly()
        // {
        //    this.controller.WithCallTo(x => x.Index())
        //        .ShouldRenderView("Index")
        //        .WithModel<ArticleSitePreviewViewModel>(
        //            vm =>
        //            {
        //                Assert.AreEqual(ArticleDbRepository.GetArticles().ToList()[vm.Id - 1], vm.Id);
        //            }).AndNoModelErrors();
        // }
    }
}