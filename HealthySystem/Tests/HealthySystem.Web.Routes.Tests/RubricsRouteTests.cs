namespace HealthySystem.Web.Routes.Tests
{
    using System.Web.Routing;
    using HealthySystem.Web.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;

    [TestClass]
    public class RubricsRouteTests
    {
        [TestMethod]
        public void TestRubricId()
        {
            var url = "/121123";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(url)
                .To<HomeController>(x => x.Index());
        }
    }
}