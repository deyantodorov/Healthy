namespace HealthySystem.Services.Data.Contracts
{
    using System.Linq;
    using HealthySystem.Data.Models;

    public interface IImageService
    {
        IQueryable<Image> GetAll();
    }
}