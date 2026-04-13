using ApplicationLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationLayer.Interfaces.RepoInterfaces
{
    public interface IReadOnlyRepository<T> where T : class
    {
        IQueryable<T> Query();
        Task<T?> GetByIdAsync(int id, params string[] includes);
        Task<T?> GetByIdAsync(Guid id, params string[] includes);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, params string[] includes);
        Task<List<T>> GetAllAsync(params string[] includes);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, params string[] includes);
        Task<PagedResponseDto<T>> GetPagedAsync(
                int pageNumber,
                int pageSize,
                Expression<Func<T, bool>>? filter = null,
                Expression<Func<T, object>>? orderBy = null,
                bool ascending = true);
    }
}
