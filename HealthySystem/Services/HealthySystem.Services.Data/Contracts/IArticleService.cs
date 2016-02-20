namespace HealthySystem.Services.Data.Contracts
{
    using System.Linq;
    using HealthySystem.Data.Models;

    public interface IArticleService
    {
        IQueryable<Article> GetAll();

        IQueryable<Article> GetAllFiltered(string order, string filter, string search, int page = 1);
    }
}