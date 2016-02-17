namespace HealthySystem.Data.Importer
{
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
            string file = "images.txt";

            var fileImporter = new FileTextImporter();
            var images = fileImporter.ReadImagesFromFile(path + "/" + file);

            for (int i = 0; i < images.Count; i++)
            {
                context.Images.Add(images[i]);

                if (i % 20 == 0)
                {
                    context.SaveChanges();
                }
            }

            context.SaveChanges();
        }
    }
}
