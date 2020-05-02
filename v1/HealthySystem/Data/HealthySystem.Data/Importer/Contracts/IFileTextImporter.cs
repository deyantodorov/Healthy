namespace HealthySystem.Data.Importer.Contracts
{
    using System.Collections.Generic;
    using HealthySystem.Data.Models;

    public interface IFileTextImporter
    {
        List<Image> ReadImagesFromFile(string file);

        List<Tag> ReadTagsFromFile(string file);

        List<Rubric> ReadRubricsFromFile(string file);

        List<Article> ReadArticleFromFile(string inputFile);
    }
}
