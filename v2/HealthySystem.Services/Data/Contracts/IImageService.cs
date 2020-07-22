using System.Linq;

using HealthySystem.Data.Models;

namespace HealthySystem.Services.Data.Contracts
{
    public interface IImageService
    {
        IQueryable<Image> GetAll();

        Image GetById(int id);

        void Add(Image image);

        void Update(Image image);

        void Delete(Image image);
    }
}