﻿namespace HealthySystem.Data.Common
{
    using System;
    using System.Linq;

    using HealthySystem.Data.Common.Models;
    using HealthySystem.Data.Contracts;

    using Microsoft.EntityFrameworkCore;

    // TODO: Why BaseModel<int> instead BaseModel<TKey>?
    public class DbRepository<T> : IDbRepository<T>
        where T : BaseModel<int>
    {
        public DbRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", nameof(context));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        private DbSet<T> DbSet { get; }

        private DbContext Context { get; }

        public IQueryable<T> All()
        {
            return this.DbSet.Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return this.DbSet;
        }

        public T GetById(int id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
        }

        public void HardDelete(T entity)
        {
            this.DbSet.Remove(entity);
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }
    }
}