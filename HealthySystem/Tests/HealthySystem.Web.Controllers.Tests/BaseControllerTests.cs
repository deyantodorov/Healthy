namespace HealthySystem.Web.Controllers.Tests
{
    using System.Linq;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Services.Web.Contracts;
    using HealthySystem.Web.Controllers.Tests.Db;
    using HealthySystem.Web.Infrastructure.Mapping;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public abstract class BaseControllerTests
    {
        protected Mock<IArticleService> ArticleServiceMock { get; private set; }

        protected Mock<IRubricService> RubricServiceMock { get; private set; }

        protected Mock<ITagService> TagServiceMock { get; private set; }

        protected Mock<IImageService> ImageServiceMock { get; private set; }

        protected Mock<ICacheService> CacheServiceMock { get; private set; }

        protected Mock<IHtmlSecuritySanitizer> SecuritySanitizer { get; private set; }

        protected Mock<ITransliterator> Transliterator { get; private set; }

        private AutoMapperConfig AutoMapperConfig { get; set; }

        [TestInitialize]
        public void Init()
        {
            this.AutoMapperConfig = new AutoMapperConfig();
            this.AutoMapperConfig.Execute(typeof(HomeController).Assembly);

            this.CacheServiceMock = new Mock<ICacheService>();
            this.CacheServiceMock.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<object>, It.IsAny<int>()))
                .Returns(FakeDbRepository.GetArticles());

            this.ArticleServiceMock = new Mock<IArticleService>();
            this.ArticleServiceMock.Setup(x => x.GetAll()).Returns(FakeDbRepository.GetArticles);
            this.ArticleServiceMock.Setup(x => x.AnyByTitle(It.IsAny<string>())).Returns(false);
            this.ArticleServiceMock.Setup(x => x.GetPublishedFromTagByAlias(It.IsAny<string>(), 1, 1)).Returns(FakeDbRepository.GetArticles);
            this.ArticleServiceMock.Setup(x => x.HasImageId(-1)).Returns(true);
            this.ArticleServiceMock.Setup(x => x.HasImageId(1)).Returns(false);

            this.RubricServiceMock = new Mock<IRubricService>();
            this.RubricServiceMock.Setup(x => x.GetAll()).Returns(FakeDbRepository.GetRubrics());

            this.TagServiceMock = new Mock<ITagService>();
            this.TagServiceMock.Setup(x => x.GetAll()).Returns(FakeDbRepository.GetTags);

            this.ImageServiceMock = new Mock<IImageService>();
            this.ImageServiceMock.Setup(x => x.GetById(1)).Returns(FakeDbRepository.GetImages().FirstOrDefault());
            this.ImageServiceMock.Setup(x => x.GetAll()).Returns(FakeDbRepository.GetImages());

            this.Transliterator = new Mock<ITransliterator>();
            this.Transliterator.Setup(x => x.GetLetterInEnglish(It.IsAny<char>())).Returns(It.IsAny<string>());
            this.Transliterator.Setup(x => x.GetTextInEnglish(It.IsAny<string>())).Returns(It.IsAny<string>());

            this.SecuritySanitizer = new Mock<IHtmlSecuritySanitizer>();
            this.SecuritySanitizer.Setup(x => x.Clean(It.IsAny<string>())).Returns(It.IsAny<string>());
        }
    }
}
