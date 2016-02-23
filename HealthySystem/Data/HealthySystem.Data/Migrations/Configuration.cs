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
            // TODO: Set to False in production
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
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