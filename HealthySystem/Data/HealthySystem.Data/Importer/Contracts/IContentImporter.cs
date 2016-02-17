namespace HealthySystem.Data.Importer.Contracts
{
    public interface IContentImporter
    {
        void Import(ApplicationDbContext context);
    }
}