namespace HealthySystem.Data.Importer
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Hosting;
    using HealthySystem.Data.Importer.Contracts;

    public class ImportTags : IDataImporter
    {
        public void Import(ApplicationDbContext context)
        {
            if (context.Tags.Any())
            {
                return;
            }

            string path = HostingEnvironment.MapPath("~/ImportData");
            path = path + "/" + "tags.txt";

            var fileImporter = new FileTextImporter();
            var tags = fileImporter.ReadTagsFromFile(path);

            for (int i = 0; i < tags.Count; i++)
            {
                context.Tags.AddOrUpdate(tags[i]);

                if (i % 20 == 0)
                {
                    context.SaveChanges();
                }
            }

            context.SaveChanges();
        }
    }
}