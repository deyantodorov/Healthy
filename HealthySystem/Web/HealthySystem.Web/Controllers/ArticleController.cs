namespace HealthySystem.Web.Controllers
{
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using HealthySystem.Common;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Web.Infrastructure.Images;
    using HealthySystem.Web.Infrastructure.Mapping;
    using HealthySystem.Web.ViewModels;

    public class ArticleController : SiteController
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public ActionResult Index(string alias)
        {
            var article = this.articleService.GetAll()
                .Where(x => x.Alias == alias.Trim())
                .To<ArticleSitePageViewModel>()
                .SingleOrDefault();

            if (article == null)
            {
                return this.HttpNotFound();
            }

            if (article.Content.Length > WebConstants.ArticleTextSplitLength + WebConstants.ArticleTextSplitLengthExtra)
            {
                article.Content = this.InjectAdsInContent(article.Content);
            }

            article.Image = Images.GetImageFromCache(article.Image, WebConstants.ImageWidth, WebConstants.ImageMaxHeight);

            this.ViewBag.Tags = this.GetTags();

            return this.View(article);
        }

        private string InjectAdsInContent(string content)
        {
            var newArticleContent = content;
            var length = WebConstants.ArticleTextSplitLength;
            var symbol = newArticleContent[length];

            while (symbol != ' ')
            {
                length++;
                symbol = newArticleContent[length];
            }

            var first = newArticleContent.Substring(0, length);
            var second = newArticleContent.Substring(length, newArticleContent.Length - length);

            var builder = new StringBuilder();
            builder.Append(first);
            builder.AppendLine("<span class=\"au-article-middle clearfix\">");
            builder.AppendLine("<script async src=\"//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js\"></script>");
            builder.AppendLine("<ins class=\"adsbygoogle\"");
            builder.AppendLine("style=\"display:block\"");
            builder.AppendLine("data-ad-client=\"ca-pub-4597731759312348\"");
            builder.AppendLine("data-ad-slot=\"8564117912\"");
            builder.AppendLine("data-ad-format=\"auto\"></ins>");
            builder.AppendLine("<script>");
            builder.AppendLine("(adsbygoogle = window.adsbygoogle || []).push({});");
            builder.AppendLine("</script>");
            builder.AppendLine("</span>");
            builder.Append(second);

            return builder.ToString();
        }
    }
}