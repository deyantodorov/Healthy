namespace HealthySystem.Services.Data.Contracts
{
    using System.Linq;
    using HealthySystem.Data.Models;

    public interface IArticleService
    {
        IQueryable<Article> GetAll();

        Article GetById(int id);

        IQueryable<Article> GetPublishedFromRubricByAlias(string alias, int pageSize, int toSkip);

        IQueryable<Article> GetPublishedFromTagByAlias(string alias, int pageSize, int toSkip);

        bool AnyByTitle(string title);

        bool AnyByTitleAndId(string title, int id);

        bool HasImageId(int imageId);

        bool HasRubricId(int rubricId);

        void Add(Article article);

        void Update(Article article);

        void Delete(Article article);
    }
}