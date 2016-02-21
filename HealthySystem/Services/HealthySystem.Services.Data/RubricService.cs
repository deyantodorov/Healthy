namespace HealthySystem.Services.Data
{
    using System.Linq;
    using HealthySystem.Data.Common;
    using HealthySystem.Data.Models;
    using HealthySystem.Services.Data.Contracts;

    public class RubricService : IRubricService
    {
        private readonly IDbRepository<Rubric> rubrics;

        public RubricService(IDbRepository<Rubric> rubrics)
        {
            this.rubrics = rubrics;
        }

        public IQueryable<Rubric> GetAll()
        {
            return this.rubrics.All();
        }
    }
}