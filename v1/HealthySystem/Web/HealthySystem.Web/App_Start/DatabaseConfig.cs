namespace HealthySystem.Web
{
    using System.Data.Entity;
    using HealthySystem.Data;
    using HealthySystem.Data.Migrations;

    public class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }
    }
}
