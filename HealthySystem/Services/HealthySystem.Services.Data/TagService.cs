namespace HealthySystem.Services.Data
{
    using System;
    using System.Linq;
    using HealthySystem.Data.Common;
    using HealthySystem.Data.Models;
    using HealthySystem.Services.Data.Contracts;

    public class TagService : ITagService
    {
        private readonly IDbRepository<Tag> tags;

        public TagService(IDbRepository<Tag> tags)
        {
            this.tags = tags;
        }

        public IQueryable<Tag> GetAll()
        {
            return this.tags.All();
        }

        public Tag GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "Name can't be null or empty");
            }

            return this.tags.All().FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }
    }
}