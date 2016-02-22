namespace HealthySystem.Services.Data.Contracts
{
    using System.Linq;
    using HealthySystem.Data.Models;

    public interface ICommentService
    {
        IQueryable<Comment> GetAll();

        IQueryable<Comment> GetAllForArticle(int articleId);

        void Add(Comment comment);
    }
}