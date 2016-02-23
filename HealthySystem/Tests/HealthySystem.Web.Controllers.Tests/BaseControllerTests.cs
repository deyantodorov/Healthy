namespace HealthySystem.Web.Controllers.Tests
{
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Services.Web.Contracts;
    using HealthySystem.Web.Infrastructure.Mapping;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public abstract class BaseControllerTests
    {
        protected Mock<IArticleService> articleServiceMock;
        protected Mock<IRubricService> rubricServiceMock;
        protected Mock<ITagService> tagServiceMock;
        protected Mock<ICacheService> cacheServiceMock;
        protected AutoMapperConfig autoMapperConfig;

        [TestInitialize]
        public void Init()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            this.autoMapperConfig.Execute(typeof(HomeController).Assembly);

            this.cacheServiceMock = new Mock<ICacheService>();
            this.cacheServiceMock.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<object>, It.IsAny<int>()))
                .Returns(FakeDbRepository.GetArticles());

            this.articleServiceMock = new Mock<IArticleService>();
            this.articleServiceMock.Setup(x => x.AnyByTitle(It.IsAny<string>())).Returns(false);
            this.articleServiceMock.Setup(x => x.GetPublishedFromTagByAlias(It.IsAny<string>(), 1, 1))
                .Returns(FakeDbRepository.GetArticles);

            this.rubricServiceMock = new Mock<IRubricService>();
            this.rubricServiceMock.Setup(x => x.GetAll()).Returns(FakeDbRepository.GetRubrics());

            this.tagServiceMock = new Mock<ITagService>();
            this.tagServiceMock.Setup(x => x.GetAll()).Returns(FakeDbRepository.GetTags);
        }
    }
}
