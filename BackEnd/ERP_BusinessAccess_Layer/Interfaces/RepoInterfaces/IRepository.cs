using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer.Interfaces.RepoInterfaces
{
    public interface IRepository<T> : IReadOnlyRepository<T> where T : class
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(T item);
        Task<T?> FindAsync(int id);

    }
}
