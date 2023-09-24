using Bootcamp.Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        //IGenericRepository<Item> GenericRepository<Item>
        IGenericRepository<T> GenericRepository<T>() where T : class;
        Task CommitAsync(CancellationToken cancellationToken);

    }
}
