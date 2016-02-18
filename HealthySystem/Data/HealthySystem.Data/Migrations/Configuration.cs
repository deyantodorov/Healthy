namespace HealthySystem.Data.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using HealthySystem.Data.Importer;
    using HealthySystem.Data.Importer.Contracts;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var importCommands = new List<IDataImporter>()
            {
                new ImportRoles(),
                new ImportUsers(),
                new ImportImages(),
                new ImportTags(),
                new ImportRubrics()
            };

            foreach (var contentImporter in importCommands)
            {
                contentImporter.Import(context);
            }
        }
    }
}