namespace HealthySystem.Data.Importer
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Hosting;
    using HealthySystem.Data.Importer.Contracts;

    public class ImportImages : IDataImporter
    {
        public void Import(ApplicationDbContext context)
        {
            if (context.Images.Any())
            {
                return;
            }

            string path = HostingEnvironment.MapPath("~/ImportData");
            path = path + "/" + "images.txt";

            var fileImporter = new FileTextImporter();
            var images = fileImporter.ReadImagesFromFile(path);

            for (int i = 0; i < images.Count; i++)
            {
                context.Images.AddOrUpdate(images[i]);

                if (i % 20 == 0)
                {
                    context.SaveChanges();
                }
            }

            context.SaveChanges();
        }
    }
}
