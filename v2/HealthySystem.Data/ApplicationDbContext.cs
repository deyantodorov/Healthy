using System;
using System.Linq;

using HealthySystem.Data.Contracts;
using HealthySystem.Data.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthySystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Rubric> Rubrics { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<TagArticles> TagArticles { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Tag>()
                .HasIndex(e => e.Name)
                .IsUnique();

            builder.Entity<Tag>()
                .HasIndex(e => e.Alias)
                .IsUnique();

            builder.Entity<Rubric>()
                .HasIndex(e => e.Name)
                .IsUnique();

            builder.Entity<Rubric>()
                .HasIndex(e => e.Alias)
                .IsUnique();

            builder.Entity<Article>()
                .HasIndex(e => e.Alias)
                .IsUnique();

            builder.Entity<TagArticles>()
                .HasKey(e => new { e.TagId, e.ArticleId });
        }

        private void ApplyAuditInfoRules()
        {
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.UtcNow;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}