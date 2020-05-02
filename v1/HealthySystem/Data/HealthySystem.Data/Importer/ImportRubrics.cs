namespace HealthySystem.Data.Importer
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Hosting;
    using HealthySystem.Data.Importer.Contracts;

    public class ImportRubrics : IDataImporter
    {
        public void Import(ApplicationDbContext context)
        {
            if (context.Rubrics.Any())
            {
                return;
            }

            string path = HostingEnvironment.MapPath("~/ImportData");
            path = path + "/" + "rubrics.txt";

            var fileImporter = new FileTextImporter();
            var rubrics = fileImporter.ReadRubricsFromFile(path);

            for (int i = 0; i < rubrics.Count; i++)
            {
                context.Rubrics.AddOrUpdate(rubrics[i]);

                if (i % 20 == 0)
                {
                    context.SaveChanges();
                }
            }

            context.SaveChanges();
        }
    }
}