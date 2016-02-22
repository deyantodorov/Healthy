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

        public void Add(Rubric rubric)
        {
            this.rubrics.Add(rubric);
            this.rubrics.Save();
        }

        public void Update(Rubric rubric)
        {
            var rubricToUpdate = this.GetById(rubric.Id);

            rubricToUpdate.Name = rubric.Name;
            rubricToUpdate.Description = rubric.Description;
            rubricToUpdate.Alias = rubric.Alias;
            rubricToUpdate.ParentId = rubric.ParentId;

            this.rubrics.Save();
        }

        public void Delete(Rubric rubric)
        {
            this.rubrics.Delete(rubric);
            this.rubrics.Save();
        }

        public Rubric GetById(int id)
        {
            return this.rubrics.GetById(id);
        }

        public bool IsParent(int id)
        {
            return this.rubrics.AllWithDeleted().Any(x => x.ParentId == id);
        }
    }
}