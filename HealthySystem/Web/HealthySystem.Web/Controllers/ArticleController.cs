namespace HealthySystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using HealthySystem.Common;
    using HealthySystem.Data.Models;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Services.Web.Contracts;
    using HealthySystem.Web.Infrastructure.Mapping;
    using HealthySystem.Web.ViewModels;
    using Microsoft.AspNet.Identity;

    public class ArticleController : SiteController
    {
        private readonly IArticleService articleService;
        private readonly ICommentService commentService;
        private readonly IHtmlSecuritySanitizer htmlSecuritySanitizer;

        public ArticleController(IArticleService articleService, ICommentService commentService, IHtmlSecuritySanitizer htmlSecuritySanitizer)
        {
            this.articleService = articleService;
            this.commentService = commentService;
            this.htmlSecuritySanitizer = htmlSecuritySanitizer;
        }

        [HttpGet]
        public ActionResult Index(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                return this.RedirectToActionPermanent("Index", "Home");
            }

            if (this.TempData.ContainsKey("DoubleComment"))
            {
                this.ViewBag.DoubleComment = this.TempData["DoubleComment"];
                this.TempData.Remove("DoubleComment");
            }

            // TODO: Bug in Article and AdSense display
            // var cacheName = alias.Replace("-", string.Empty);
            // var article = this.Cache.Get(
            //    cacheName,
            //    () => this.articleService.GetAll()
            //    .Where(x => x.Alias.ToLower().Equals(alias.ToLower().Trim()))
            //    .To<ArticleSitePageViewModel>()
            //    .SingleOrDefault(),
            //    WebConstants.Min5);

            var article = this.articleService.GetAll()
                .Where(x => x.Alias.ToLower().Equals(alias.ToLower().Trim()))
                .To<ArticleSitePageViewModel>()
                .SingleOrDefault();

            if (article == null)
            {
                return this.HttpNotFound();
            }

            var otherArticles = this.Cache.Get(
                "SeeMore" + article.RubricId,
                () => this.articleService.GetAll()
                .Where(x => x.IsPublished && x.PublishDate <= DateTime.Now && x.Id != article.Id && x.RubricId == article.RubricId)
                .OrderByDescending(x => x.CreatedOn)
                .Take(WebConstants.SiteOtherArticlesSize)
                .To<ArticleSiteShortPreviewViewModel>()
                .ToList(),
                WebConstants.Min15);

            article.OtherArticles = otherArticles;

            if (article.Content.Length > WebConstants.ArticleTextSplitLength + WebConstants.ArticleTextSplitLengthExtra)
            {
                article.Content = this.InjectAdsInContent(article.Content);
            }

            this.ViewBag.Tags = this.GetTags();

            return this.View(article);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(CommentViewModel comment)
        {
            if (comment != null && this.ModelState.IsValid)
            {
                comment.Content = this.htmlSecuritySanitizer.Clean(comment.Content);
                comment.Author = this.htmlSecuritySanitizer.Clean(comment.Author);
                comment.ArticleAlias = this.htmlSecuritySanitizer.Clean(comment.ArticleAlias);

                var alias = comment.ArticleAlias.Split('/').Last().ToLower();
                var userId = this.User.Identity.GetUserId();

                var article = this.articleService.GetAll()
                    .FirstOrDefault(x => x.Alias.Equals(alias));

                if (article == null)
                {
                    return this.RedirectToAction("Index");
                }

                if (article.Comments.LastOrDefault(x => x.AuthorId == userId) != null)
                {
                    // Last comment is again by me
                    this.TempData.Add("DoubleComment", "Моля, изчакайте да ви отоговорят преди да коментирате отново!");
                    return this.Redirect("/" + comment.ArticleAlias + "#comments");
                }

                var newComment = new Comment()
                {
                    Content = comment.Content,
                    AuthorId = userId,
                    ArticleId = article.Id
                };

                this.commentService.Add(newComment);

                return this.Redirect("/" + comment.ArticleAlias + "#comments");
            }

            return this.RedirectToAction("Index");
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