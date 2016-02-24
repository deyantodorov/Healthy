namespace HealthySystem.Services.Web.Tests
{
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Web.Areas.Manager.Controllers;
    using HealthySystem.Web.Infrastructure.Mapping;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public abstract class BaseServiceTest
    {
        private AutoMapperConfig autoMapperConfig;
        private Mock<IArticleService> articleServiceMock;
        private Mock<IRubricService> rubricServiceMock;
        private Mock<ITagService> tagServiceMock;

        public void Init()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            this.autoMapperConfig.Execute(typeof(HomeController).Assembly);

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
