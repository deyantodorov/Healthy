using System.Linq;

using HealthySystem.Data.Contracts;
using HealthySystem.Data.Models;
using HealthySystem.Services.Data.Contracts;

namespace HealthySystem.Services.Data
{
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

        public void Add(Image image)
        {
            this.images.Add(image);
            this.images.Save();
        }

        public void Update(Image image)
        {
            var imageForUpdate = this.GetById(image.Id);

            imageForUpdate.ImageDescription = image.ImageDescription;

            this.images.Save();
        }

        public void Delete(Image image)
        {
            this.images.Delete(image);
            this.images.Save();
        }
    }
}
