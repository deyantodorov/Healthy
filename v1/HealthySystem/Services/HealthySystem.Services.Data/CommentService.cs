﻿namespace HealthySystem.Services.Data
{
    using System.Linq;
    using HealthySystem.Data.Common;
    using HealthySystem.Data.Models;
    using HealthySystem.Services.Data.Contracts;

    public class CommentService : ICommentService
    {
        private readonly IDbRepository<Comment> comments;

        public CommentService(IDbRepository<Comment> comments)
        {
            this.comments = comments;
        }

        public IQueryable<Comment> GetAll()
        {
            return this.comments.All();
        }

        public IQueryable<Comment> GetAllForArticle(int articleId)
        {
            return this.comments.All().Where(x => x.ArticleId == articleId);
        }

        public void Add(Comment comment)
        {
            this.comments.Add(comment);
            this.comments.Save();
        }
    }
}