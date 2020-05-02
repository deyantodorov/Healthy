namespace HealthySystem.Data.Importer.Contracts
{
    public interface IDataImporter
    {
        void Import(ApplicationDbContext context);
    }
}