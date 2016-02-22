namespace HealthySystem.Services.Data.Contracts
{
    using System.Linq;
    using HealthySystem.Data.Models;

    public interface IImageService
    {
        IQueryable<Image> GetAll();

        Image GetById(int id);

        void Add(Image image);

        void Update(Image image);

        void Delete(Image image);
    }
}