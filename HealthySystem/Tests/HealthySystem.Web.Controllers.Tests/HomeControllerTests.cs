namespace HealthySystem.Web.Controllers.Tests
{
    using HealthySystem.Services.Web.Contracts;
    using HealthySystem.Web.Infrastructure.Mapping;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(HomeController).Assembly);

            var cacheServiceMock = new Mock<ICacheService>();
        }
    }
}