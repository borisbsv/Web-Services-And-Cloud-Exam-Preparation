using System;
using System.Data.Entity;
using System.Linq;

using FLExtensions.Common;

namespace BullsAndCows.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected IBullsAndCowsDbContext context { get; set; }
        protected IDbSet<T> DbSet { get; set; }

        public GenericRepository(IBullsAndCowsDbContext context)
        {
            context.ThrowIfNull("Pesho castna.");

            this.context = context;
            this.DbSet = this.context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            var entry = this.context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public IQueryable<T> All()
        {
            return this.DbSet.AsQueryable();
        }

        public T Attach(T entity)
        {
            return this.DbSet.Attach(entity);
        }

        public void Delete(object id)
        {
            var entry = this.GetById(id);

            if (entry != null)
            {
                this.Delete(entry);
            }
        }

        public void Delete(T entity)
        {
            var entry = this.context.Entry(entity);

            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public void Detach(T entity)
        {
            var entry = this.context.Entry(entity);

            entry.State = EntityState.Detached;
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Update(T entity)
        {
            var entry = this.context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
