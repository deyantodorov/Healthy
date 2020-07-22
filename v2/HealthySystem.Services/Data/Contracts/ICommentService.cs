using System.Linq;

using HealthySystem.Data.Models;

namespace HealthySystem.Services.Data.Contracts
{
    public interface ICommentService
    {
        IQueryable<Comment> GetAll();

        IQueryable<Comment> GetAllForArticle(int articleId);

        void Add(Comment comment);
    }
}