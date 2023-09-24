using Bootcamp.Application.Common.Interfaces;
using Bootcamp.Application.Common.Interfaces.Repository;
using Bootcamp.Infrastructure.Persistence;
using Bootcamp.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Infrastructure.Services
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly BootcampDbContext _context;
        public UnitOfWork(BootcampDbContext context)
        {
            _context = context;

        }


        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {

            await _context.SaveChangesAsync(cancellationToken);

        }


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
