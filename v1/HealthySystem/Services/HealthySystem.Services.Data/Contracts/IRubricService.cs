namespace HealthySystem.Services.Data.Contracts
{
    using System.Linq;
    using HealthySystem.Data.Models;

    public interface IRubricService
    {
        IQueryable<Rubric> GetAll();

        void Add(Rubric rubric);

        void Update(Rubric rubric);

        void Delete(Rubric rubric);

        Rubric GetById(int id);

        bool IsParent(int id);
    }
}