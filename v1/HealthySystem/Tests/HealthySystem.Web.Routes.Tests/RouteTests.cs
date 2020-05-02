namespace HealthySystem.Web.Routes.Tests
{
    using System.Web.Routing;
    using HealthySystem.Web.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;
    using MvcRouteTester.Assertions;

    [TestClass]
    public class RouteTests
    {
        [TestMethod]
        public void TestHomeRoute()
        {
            var url = "/";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(url)
                .To<HomeController>(x => x.Index());
        }

        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void TestHomeNotFound()
        {
            var url = "/home/notfound";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(url)
                .To<HomeController>(x => x.Index());
        }

        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void TestHomeError()
        {
            var url = "/home/error";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(url)
                .To<HomeController>(x => x.Index());
        }

        [TestMethod]
        public void TestRubricRoute()
        {
            var url = "/r/alias";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(url)
                .To<RubricController>(x => x.Index("alias", null));
        }

        [TestMethod]
        public void TestTagRoute()
        {
            var url = "/t/tag";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(url)
                .To<TagController>(x => x.Index("tag", null));
        }

        [TestMethod]
        public void TestArticleRoute()
        {
            var url = "/r/rubric/alias";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(url)
                .To<ArticleController>(x => x.Index("alias"));
        }

        [TestMethod]
        public void TestSitemapRoute()
        {
            var url = "/sitemap";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(url)
                .To<SitemapController>(x => x.Index());
        }
    }
}