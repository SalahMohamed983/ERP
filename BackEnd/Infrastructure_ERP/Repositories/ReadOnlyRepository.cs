
using ApplicationLayer.Common;
using ApplicationLayer.Interfaces.RepoInterfaces;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfrastructureLayer.Repositories
{
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class
    {
        protected readonly ERPContext _context;

        public ReadOnlyRepository(ERPContext context)
        {
            _context = context;
        }

        protected IQueryable<T> ApplyIncludes(IQueryable<T> query, string[] includes)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        public async Task<T?> GetByIdAsync(int id, params string[] includes)
        {
            var query = ApplyIncludes(_context. Set<T>(), includes);
            return await query.AsNoTracking()
                .FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        public async Task<T?> GetByIdAsync(Guid id, params string[] includes)
        {
            var query = ApplyIncludes(_context.Set<T>(), includes);
            return await query.AsNoTracking()
                .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }
        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, params string[] includes )
        {
            var query = ApplyIncludes(_context.Set<T>(), includes);
            return await query.AsNoTracking().FirstOrDefaultAsync(filter);
        }

        public async Task<List<T>> GetAllAsync(params string[] includes)
        {
            var query = ApplyIncludes(_context.Set<T>(), includes);
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, params string[] includes)
        {
            var query = ApplyIncludes(_context.Set<T>(), includes);
            return await query.AsNoTracking().Where(filter).ToListAsync();
        }



        public async Task<PagedResponseDto<T>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, object>>? orderBy = null,
        bool ascending = true)
        {
            // Build query
            var query = Query();

            // Apply filter if provided
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply ordering
            if (orderBy != null)
            {
                query = ascending
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);
            }

            // Apply pagination
            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Calculate total pages
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResponseDto<T>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }

        public  IQueryable<T> Query()
        {
            return _context.Set<T>().AsNoTracking().AsQueryable();
        }

    }
}
