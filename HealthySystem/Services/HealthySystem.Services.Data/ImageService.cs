namespace HealthySystem.Services.Data
{
    using System.Linq;
    using HealthySystem.Data.Common;
    using HealthySystem.Data.Models;
    using HealthySystem.Services.Data.Contracts;

    public class ImageService : IImageService
    {
        private readonly IDbRepository<Image> images;

        public ImageService(IDbRepository<Image> images)
        {
            this.images = images;
        }

        public IQueryable<Image> GetAll()
        {
            return this.images.All();
        }

        public Image GetById(int id)
        {
            return this.images.GetById(id);
        }
    }
}
