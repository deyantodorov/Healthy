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
        public List<Image> ReadImagesFromFile(string file)
        {
            var images = new List<Image>();

            using (var input = new StreamReader(file))
            {
                string line = input.ReadLine();
                while (line != null)
                {
                    var currentWords = line.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

                    var image = new Image()
                    {
                        ImagePath = currentWords[1],
                        ImageDescription = currentWords[2]
                    };

                    images.Add(image);

                    line = input.ReadLine();
                }
            }

            return images;
        }

        public SortedSet<string> ReadTagFromFile(string inputFile)
        {
            var result = new SortedSet<string>();

            var input = new StreamReader(inputFile);

            using (input)
            {
                string line = input.ReadLine();

                while (line != null)
                {
                    var currentWords = line.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string textToAdd = GetWords(currentWords);
                    result.Add(textToAdd);

                    line = input.ReadLine();
                }
            }

            return result;
        }

        public List<string> ReadRubricFromFile(string inputFile)
        {
            var result = new List<string>();

            var input = new StreamReader(inputFile);

            using (input)
            {
                string line = input.ReadLine();

                while (line != null)
                {
                    var currentWords = line.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string textToAdd = GetWords(currentWords);
                    result.Add(textToAdd);

                    line = input.ReadLine();
                }
            }

            return result;
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