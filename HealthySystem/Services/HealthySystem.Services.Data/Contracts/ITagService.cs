namespace HealthySystem.Services.Data.Contracts
{
    using System.Linq;
    using HealthySystem.Data.Models;

    public interface ITagService
    {
        IQueryable<Tag> GetAll();

        Tag GetByName(string name);
    }
}
