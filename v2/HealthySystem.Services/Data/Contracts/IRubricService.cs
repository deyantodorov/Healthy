using System.Linq;

using HealthySystem.Data.Models;

namespace HealthySystem.Services.Data.Contracts
{
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