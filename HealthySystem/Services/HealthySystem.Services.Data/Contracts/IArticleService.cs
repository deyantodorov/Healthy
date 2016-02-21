namespace HealthySystem.Services.Data.Contracts
{
    using System.Linq;
    using HealthySystem.Data.Models;

    public interface IArticleService
    {
        IQueryable<Article> GetAll();

        Article GetById(int id);

        bool AnyByTitle(string title);

        bool AnyByTitleAndId(string title, int id);

        void Add(Article article);

        void Update(Article article);

        void Delete(Article article);
    }
}