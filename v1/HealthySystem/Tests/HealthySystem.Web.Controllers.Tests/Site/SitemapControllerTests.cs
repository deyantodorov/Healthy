﻿namespace HealthySystem.Web.Controllers.Tests.Site
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SitemapControllerTests : BaseControllerTests
    {
        private SitemapController controller;

        [TestInitialize]
        public void Setup()
        {
            this.controller = new SitemapController(this.ArticleServiceMock.Object, this.RubricServiceMock.Object)
            {
                Cache = this.CacheServiceMock.Object
            };
        }
    }
}
