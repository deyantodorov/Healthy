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
            var importCommands = new List<IContentImporter>()
            {
                new ImportRoles(),
                new ImportUsers()
            };

            foreach (var contentImporter in importCommands)
            {
                contentImporter.Import(context);
            }
        }
    }
}