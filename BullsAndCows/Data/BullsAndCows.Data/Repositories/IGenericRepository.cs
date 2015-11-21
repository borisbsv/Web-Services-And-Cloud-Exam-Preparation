using System;
using System.Linq;

namespace BullsAndCows.Data.Repositories
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        IQueryable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        T Attach(T entity);

        void Detach(T entity);

        int SaveChanges();
    }
}
