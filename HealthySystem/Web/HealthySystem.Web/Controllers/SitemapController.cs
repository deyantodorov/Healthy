namespace HealthySystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using HealthySystem.Common;
    using HealthySystem.Services.Data.Contracts;

    public class SitemapController : SiteController
    {
        private readonly IArticleService articleService;
        private readonly IRubricService rubricService;

        public SitemapController(IArticleService articleService, IRubricService rubricService)
        {
            this.articleService = articleService;
            this.rubricService = rubricService;
        }

        [HttpGet]
        [OutputCache(Duration = WebConstants.Min60)]
        public ActionResult Index()
        {
            var builder = new StringBuilder();

            this.AppendStart(builder);
            this.AppendSite(builder);
            this.AppendRubrics(builder);
            this.AppendArticles(builder);
            this.AppendTags(builder);
            this.AppendEnd(builder);

            return this.Content(builder.ToString(), "text/xml");
        }

        private void AppendStart(StringBuilder builder)
        {
            builder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            builder.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");
        }

        private void AppendEnd(StringBuilder builder)
        {
            builder.AppendLine("</urlset>");
        }

        private void AppendSite(StringBuilder builder)
        {
            builder.AppendLine("<url>");
            builder.AppendLine("<loc>http://megazdrave.com/</loc>");
            builder.AppendLine("</url>");
        }

        private void AppendRubrics(StringBuilder builder)
        {
            var rubrics = this.rubricService
                .GetAll()
                .Select(x => new
                {
                    x.Alias
                })
                .ToList();

            foreach (var rubric in rubrics)
            {
                builder.AppendLine("<url>");
                builder.AppendLine("<loc>http://megazdrave.com/r/" + rubric.Alias + "</loc>");
                builder.AppendLine("</url>");
            }
        }

        private void AppendTags(StringBuilder builder)
        {
            var tags = this.TagService.GetAll()
                .Select(x => new
                {
                    x.Alias
                })
                .ToList();

            foreach (var tag in tags)
            {
                builder.AppendLine("<url>");
                builder.AppendLine("<loc>http://megazdrave.com/t/" + tag.Alias + "</loc>");
                builder.AppendLine("</url>");
            }
        }

        private void AppendArticles(StringBuilder builder)
        {
            var articles = this.articleService.GetAll()
                .Where(x => x.IsPublished && x.PublishDate <= DateTime.Now)
                .Select(x => new
                {
                    Alias = x.Rubric.Alias + "/" + x.Alias,
                    Date = x.PublishDate
                })
                .ToList();

            foreach (var article in articles)
            {
                builder.AppendLine("<url>");
                builder.AppendLine("<loc>http://megazdrave.com/r/" + article.Alias + "</loc>");
                builder.AppendLine("<lastmod>" + string.Format("{0:yyyy-MM-ddThh:mm:ss+03:00}", article.Date) + "</lastmod>");
                builder.AppendLine("</url>");
            }
        }
    }
}