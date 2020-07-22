﻿using System;
using System.Linq;

using HealthySystem.Data.Contracts;
using HealthySystem.Data.Models;
using HealthySystem.Services.Data.Contracts;

namespace HealthySystem.Services.Data
{
    public class ArticleService : IArticleService
    {
        private readonly IDbRepository<Article> articles;

        public ArticleService(IDbRepository<Article> articles)
        {
            this.articles = articles;
        }

        public IQueryable<Article> GetAll()
        {
            return this.articles.All();
        }

        public Article GetById(int id)
        {
            return this.articles.GetById(id);
        }

        public IQueryable<Article> GetPublishedFromRubricByAlias(string alias, int pageSize, int toSkip)
        {
            return this.articles.All()
                .Where(x => x.IsPublished && x.PublishDate <= DateTime.Now && x.Rubric.Alias.ToLower().Equals(alias.ToLower()))
                .OrderByDescending(x => x.PublishDate)
                .Skip((pageSize - 1) * toSkip)
                .Take(toSkip);
        }

        public IQueryable<Article> GetPublishedFromTagByAlias(string alias, int pageSize, int toSkip)
        {
            return this.articles.All()
                .Where(x => x.IsPublished && x.PublishDate < DateTime.UtcNow && x.TagArticles.Any(y => y.Tag.Alias.ToLower().Equals(alias.ToLower())))
                .OrderByDescending(x => x.PublishDate)
                .Skip((pageSize - 1) * toSkip)
                .Take(toSkip);
        }

        public bool AnyByTitle(string title)
        {
            return this.articles.AllWithDeleted().Any(x => x.Title.ToLower().Equals(title.ToLower()));
        }

        public bool AnyByTitleAndId(string title, int id)
        {
            this.IsNullOrEmpty(title);
            return this.articles.AllWithDeleted().Any(x => x.Title.ToLower().Equals(title.ToLower()) && x.Id != id);
        }

        public bool HasImageId(int imageId)
        {
            return this.articles.AllWithDeleted().Any(x => x.ImageId == imageId);
        }

        public bool HasRubricId(int rubricId)
        {
            return this.articles.AllWithDeleted().Any(x => x.RubricId == rubricId);
        }

        public void Add(Article article)
        {
            this.articles.Add(article);
            this.articles.Save();
        }

        public void Update(Article article)
        {
            var articleForUpdate = this.articles.GetById(article.Id);

            articleForUpdate.Title = article.Title;
            articleForUpdate.Alias = article.Alias;
            articleForUpdate.Description = article.Description;
            articleForUpdate.Content = article.Content;
            articleForUpdate.IsPublished = article.IsPublished;
            articleForUpdate.PublishDate = article.PublishDate;
            articleForUpdate.UnpublishDate = article.UnpublishDate;
            articleForUpdate.Rank = article.Rank;
            articleForUpdate.ImageId = article.ImageId;
            articleForUpdate.RubricId = article.RubricId;
            articleForUpdate.AuthorId = article.AuthorId;
            //articleForUpdate.Tags = article.Tags; // TODO: fix
            articleForUpdate.Comments = article.Comments;

            this.articles.Save();
        }

        public void Delete(Article article)
        {
            this.articles.Delete(article);
            this.articles.Save();
        }

        private void IsNullOrEmpty(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("title", "Can't be null or empty");
            }
        }
    }
}