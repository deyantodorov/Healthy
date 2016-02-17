namespace HealthySystem.Services.Data
{
    using System.Linq;
    using HealthySystem.Data.Common;
    using HealthySystem.Data.Models;
    using HealthySystem.Services.Data.Contracts;

    public class ArticleService : IArticleService
    {
        private readonly IDbRepository<Article> articles;

        public ArticleService(IDbRepository<Article> articles)
        {
            this.articles = articles;
        }

        /// <summary>
        /// Get all articles
        /// </summary>
        /// <returns>All articles without deleted</returns>
        public IQueryable<Article> GetAll()
        {
            return this.articles.All().Where(a => !a.IsDeleted);
        }
    }
}
