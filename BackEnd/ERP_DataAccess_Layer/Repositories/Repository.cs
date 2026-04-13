
using ApplicationLayer.Interfaces.RepoInterfaces;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    public class Repository<T> : ReadOnlyRepository<T>, IRepository<T> where T : class
    {
        private readonly ERPContext _context;
        public Repository(ERPContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity) => _context.Set<T>().Update(entity);

        public void Delete(T item)
        {
                _context.Set<T>().Remove(item);
        }
        public void UpdateRange(IEnumerable<T> entities)
           => _context.Set<T>().UpdateRange(entities);

        public async Task<T?> FindAsync(int id) => await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

    }
}
