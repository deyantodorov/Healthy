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

        public IQueryable<Article> GetAll()
        {
            return this.articles
                .All()
                .Where(a => !a.IsDeleted);
        }

        public IQueryable<Article> GetAllFiltered(string order, string filter, string search, int page = 1)
        {
            throw new System.NotImplementedException();
        }
    }
}
