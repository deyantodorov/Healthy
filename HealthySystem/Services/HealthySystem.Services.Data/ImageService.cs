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

        /// <summary>
        /// Get all images
        /// </summary>
        /// <returns>All images without deleted</returns>
        public IQueryable<Image> GetAll()
        {
            return this.images.All().Where(i => !i.IsDeleted);
        }
    }
}
