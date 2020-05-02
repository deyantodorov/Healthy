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

        public Tag GetById(int id)
        {
            return this.tags.GetById(id);
        }

        public void Add(Tag tag)
        {
            this.tags.Add(tag);
            this.tags.Save();
        }

        public void Update(Tag tag)
        {
            var tagForUpdate = this.GetById(tag.Id);

            tagForUpdate.Name = tag.Name;
            tagForUpdate.Alias = tag.Alias;

            this.tags.Save();
        }

        public void Delete(Tag tag)
        {
            this.tags.Delete(tag);
            this.tags.Save();
        }

        public Tag GetByName(string name)
        {
            this.IsNullOrEmpty(name);
            return this.tags.All().FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));
        }

        public bool AnyByName(string name, string alias)
        {
            this.IsNullOrEmpty(name);
            this.IsNullOrEmpty(alias);

            name = name.ToLower();
            alias = alias.ToLower();

            return this.tags.AllWithDeleted().Any(x => x.Name.ToLower().Equals(name) || x.Alias.ToLower().Equals(alias));
        }

        public bool AnyByNameId(string name, string alias, int id)
        {
            this.IsNullOrEmpty(name);
            this.IsNullOrEmpty(alias);

            name = name.ToLower();
            alias = alias.ToLower();

            var result = this.tags
                .AllWithDeleted()
                .Where(x => !x.Id.Equals(id) && (x.Name.ToLower().Equals(name) || x.Alias.ToLower().Equals(alias)))
                .ToList();

            return result.Any();
        }

        private void IsNullOrEmpty(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "Name can't be null or empty");
            }
        }
    }
}