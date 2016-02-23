namespace HealthySystem.Web.Controllers.Tests.FakeDbRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HealthySystem.Data.Models;

    public class ArticleDbRepository
    {
        public static IQueryable<Article> GetArticles()
        {
            var articles = new List<Article>()
            {
                new Article()
                {
                    Id = 1,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = null,
                    IsDeleted = false,
                    DeletedOn = null,
                    Title = "Заглавие на статия",
                    Alias = "zaglavie-na-statiya",
                    Description = "Описание на статия",
                    Content = "Съдържание на статия",
                    IsPublished = true,
                    PublishDate = DateTime.Now,
                    UnpublishDate = null,
                    Rank = 0,
                    ImageId = null,
                    RubricId = 1,
                    AuthorId = "123123",
                    Tags = new HashSet<Tag>(),
                    Comments = new HashSet<Comment>()
                },
                new Article()
                {
                    Id = 2,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = null,
                    IsDeleted = false,
                    DeletedOn = null,
                    Title = "Заглавие на статия 2",
                    Alias = "zaglavie-na-statiya 2",
                    Description = "Описание на статия 2",
                    Content = "Съдържание на статия 2",
                    IsPublished = true,
                    PublishDate = DateTime.Now,
                    UnpublishDate = null,
                    Rank = 0,
                    ImageId = null,
                    RubricId = 1,
                    AuthorId = "123123",
                    Tags = new HashSet<Tag>(),
                    Comments = new HashSet<Comment>()
                },
                new Article()
                {
                    Id = 3,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = null,
                    IsDeleted = false,
                    DeletedOn = null,
                    Title = "Заглавие на статия ",
                    Alias = "zaglavie-na-statiya ",
                    Description = "Описание на статия 3",
                    Content = "Съдържание на статия 3",
                    IsPublished = true,
                    PublishDate = DateTime.Now,
                    UnpublishDate = null,
                    Rank = 0,
                    ImageId = null,
                    RubricId = 1,
                    AuthorId = "123123",
                    Tags = new HashSet<Tag>(),
                    Comments = new HashSet<Comment>()
                },
                new Article()
                {
                    Id = 4,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = null,
                    IsDeleted = false,
                    DeletedOn = null,
                    Title = "Заглавие на статия 4",
                    Alias = "zaglavie-na-statiya 4",
                    Description = "Описание на статия 4",
                    Content = "Съдържание на статия 4",
                    IsPublished = true,
                    PublishDate = DateTime.Now,
                    UnpublishDate = null,
                    Rank = 0,
                    ImageId = null,
                    RubricId = 1,
                    AuthorId = "123123",
                    Tags = new HashSet<Tag>(),
                    Comments = new HashSet<Comment>()
                },
                new Article()
                {
                    Id = 5,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = null,
                    IsDeleted = false,
                    DeletedOn = null,
                    Title = "Заглавие на статия 5",
                    Alias = "zaglavie-na-statiya 5",
                    Description = "Описание на статия 5",
                    Content = "Съдържание на статия 5",
                    IsPublished = true,
                    PublishDate = DateTime.Now,
                    UnpublishDate = null,
                    Rank = 0,
                    ImageId = null,
                    RubricId = 1,
                    AuthorId = "123123",
                    Tags = new HashSet<Tag>(),
                    Comments = new HashSet<Comment>()
                },
                new Article()
                {
                    Id = 6,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = null,
                    IsDeleted = false,
                    DeletedOn = null,
                    Title = "Заглавие на статия 6",
                    Alias = "zaglavie-na-statiya 6",
                    Description = "Описание на статия 6",
                    Content = "Съдържание на статия 6",
                    IsPublished = true,
                    PublishDate = DateTime.Now,
                    UnpublishDate = null,
                    Rank = 0,
                    ImageId = null,
                    RubricId = 1,
                    AuthorId = "123123",
                    Tags = new HashSet<Tag>(),
                    Comments = new HashSet<Comment>()
                },
            };

            return articles.AsQueryable();
        }
    }
}
