namespace HealthySystem.Data.Importer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using HealthySystem.Data.Importer.Contracts;
    using HealthySystem.Data.Models;

    public class FileTextImporter : IFileTextImporter
    {
        private const string EmptyDbRecord = "empty";

        /// <summary>
        /// Import old DB data for Images to new data structure for Images
        /// </summary>
        /// <param name="file">file path</param>
        /// <returns>Collection of Image models ready for submit in DB</returns>
        public List<Image> ReadImagesFromFile(string file)
        {
            var images = new List<Image>();

            using (var input = new StreamReader(file))
            {
                string line = input.ReadLine();

                for (int i = 1; i <= 593; i++)
                {
                    var image = new Image();

                    if (line == null)
                    {
                        break;
                    }

                    var currentWords = line.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                    var isBlank = currentWords[0] != i.ToString();

                    if (isBlank)
                    {
                        image.ImagePath = EmptyDbRecord;
                        image.ImageDescription = EmptyDbRecord;
                        image.IsDeleted = true;
                        image.DeletedOn = DateTime.Now;
                    }
                    else
                    {
                        image.ImagePath = currentWords[1];
                        image.ImageDescription = currentWords[2];

                        line = input.ReadLine();
                    }

                    images.Add(image);
                }
            }

            return images;
        }

        /// <summary>
        /// Import old DB data for Tags to new data structure for Tags
        /// </summary>
        /// <param name="file">file path</param>
        /// <returns>Collection of Tag models ready for submit in DB</returns>
        public List<Tag> ReadTagsFromFile(string file)
        {
            var tags = new List<Tag>();

            using (var input = new StreamReader(file))
            {
                string line = input.ReadLine();

                for (int i = 1; i <= 543; i++)
                {
                    var tag = new Tag();

                    if (line == null)
                    {
                        break;
                    }

                    var currentWords = line.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                    var isBlank = currentWords[0] != i.ToString();

                    if (isBlank)
                    {
                        tag.Name = EmptyDbRecord + i;
                        tag.Alias = EmptyDbRecord + i;
                        tag.Rank = 0;
                        tag.IsDeleted = true;
                        tag.DeletedOn = DateTime.Now;
                    }
                    else
                    {
                        tag.Name = currentWords[1];
                        tag.Alias = currentWords[2];
                        tag.Rank = int.Parse(currentWords[3]);

                        line = input.ReadLine();
                    }

                    tags.Add(tag);
                }
            }

            return tags;
        }

        /// <summary>
        /// Import old DB data for Rubrics to new data structure for Rubrics
        /// </summary>
        /// <param name="file">file path</param>
        /// <returns>Collection of Rubric models ready for submit in DB</returns>
        public List<Rubric> ReadRubricsFromFile(string file)
        {
            var rubrics = new List<Rubric>();

            using (var input = new StreamReader(file))
            {
                string line = input.ReadLine();

                for (int i = 1; i <= 7; i++)
                {
                    var rubric = new Rubric();

                    if (line == null)
                    {
                        break;
                    }

                    var currentWords = line.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                    var isBlank = currentWords[0] != i.ToString();

                    if (isBlank)
                    {
                        rubric.Name = EmptyDbRecord + i;
                        rubric.Alias = EmptyDbRecord + i;
                        rubric.IsDeleted = true;
                        rubric.DeletedOn = DateTime.Now;
                    }
                    else
                    {
                        rubric.Name = currentWords[1];
                        rubric.Alias = currentWords[4];
                        rubric.Title = currentWords[5];
                        rubric.Description = currentWords[6];

                        line = input.ReadLine();
                    }

                    rubrics.Add(rubric);
                }
            }

            return rubrics;
        }

        public List<Article> ReadArticleFromFile(string inputFile)
        {
            var articles = new List<Article>();
            var input = new StreamReader(inputFile);

            using (input)
            {
                string line = input.ReadLine();

                while (line != null)
                {
                    var currentArticle = GetCurrentArticle(line);

                    if (IsArticleUnique(currentArticle, articles))
                    {
                        articles.Add(currentArticle);
                    }

                    line = input.ReadLine();
                }
            }

            return articles;
        }

        private static bool IsArticleUnique(Article currentArticle, IEnumerable<Article> articles)
        {
            return articles.All(article => !article.Title.Equals(currentArticle.Title));
        }

        private static Article GetCurrentArticle(string input)
        {
            var list = new List<string>();
            var sb = new StringBuilder();

            bool inText = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]) && !inText)
                {
                    sb.Append(input[i]);
                }
                else if (input[i] == '\'' && !inText && i - 2 >= 0 && input[i - 2] == ',' && input[i - 1] == ' ')
                {
                    inText = true;
                }
                else if (input[i] == '\'' && inText && i + 1 <= input.Length - 1 && input[i + 1] == ',')
                {
                    inText = false;
                }
                else if (inText)
                {
                    sb.Append(input[i]);
                }
                else if (input[i] == ',' && !inText)
                {
                    list.Add(sb.ToString());
                    sb.Clear();
                }
            }

            Article article = new Article
            {
                Title = list[1].Trim(),
                //Alias = Transliterator.GetTextInEnglish(list[1].Trim()),
                Description = list[2].Length >= 250 ? list[2].Substring(0, 250) : list[2],
                RubricId = GetNewRubricId(list[3]),
                Content = list[4].Trim(),
                IsPublished = true,
                //PublishDate = DateConverter.FromUnixToSystemDateTime(list[5]),
                //AddDate = list.Count < 7 ? DateConverter.FromUnixToSystemDateTime(list[5]) : DateConverter.FromUnixToSystemDateTime(list[6]),
            };

            return article;
        }

        /// <summary>
        /// Return new parent id's from current db
        /// </summary>
        private static int GetNewRubricId(string oldId)
        {
            int newId = int.Parse(oldId);

            switch (newId)
            {
                case 2: return 1; // Лечение
                case 4: return 2; // Здраве
                case 24: return 3; // Психология
                case 138: return 4; // Здравословно
                case 145: return 5; // Секс и здраве
                case 492: return 4; // Здравословно хранене
            }

            return 4;
        }

        private static string GetWords(string[] currentWords)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < currentWords.Length; i++)
            {
                string currentText;

                if (i == 0)
                {
                    currentText = currentWords[i].ToLower();
                    currentText = currentText[0].ToString().ToUpper() + currentText.Substring(1, currentText.Length - 1);
                    sb.Append(currentText);
                    sb.Append(' ');
                }
                else
                {
                    currentText = currentWords[i];
                    sb.Append(currentText);
                    sb.Append(' ');
                }
            }

            return sb.ToString().Trim();
        }
    }
}