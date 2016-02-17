namespace HealthySystem.Data.Importer.Contracts
{
    using System.Collections.Generic;
    using HealthySystem.Data.Models;

    public interface IFileTextImporter
    {
        List<Image> ReadImagesFromFile(string file);

        SortedSet<string> ReadTagFromFile(string inputFile);

        List<string> ReadRubricFromFile(string inputFile);

        List<Article> ReadArticleFromFile(string inputFile);
    }
}
