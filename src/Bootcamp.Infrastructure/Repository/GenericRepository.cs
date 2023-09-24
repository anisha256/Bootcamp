using Bootcamp.Application.Common.Interfaces.Repository;
using Bootcamp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Bootcamp.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BootcampDbContext _context;

        public GenericRepository(BootcampDbContext context)
        {
            _context = context;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(Guid? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<Guid> InsertAndGetIdAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            Guid TId = (Guid)entity.GetType().GetProperty("Id").GetValue(entity, null);
            return TId;
        }

        public async Task InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
