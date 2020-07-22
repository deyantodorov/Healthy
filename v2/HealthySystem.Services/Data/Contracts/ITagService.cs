using System.Linq;

using HealthySystem.Data.Models;

namespace HealthySystem.Services.Data.Contracts
{
    public interface ITagService
    {
        IQueryable<Tag> GetAll();

        Tag GetById(int id);

        void Add(Tag tag);

        void Update(Tag tag);

        void Delete(Tag tag);

        Tag GetByName(string name);

        bool AnyByName(string name, string alias);

        bool AnyByNameId(string name, string alias, int id);
    }
}
