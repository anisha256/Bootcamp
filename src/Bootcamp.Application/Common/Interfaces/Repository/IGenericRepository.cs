namespace Bootcamp.Application.Common.Interfaces.Repository
{
    public interface IGenericRepository<T> where T : class
    {

        Task InsertAsync(T entity);
        Task<Guid> InsertAndGetIdAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IQueryable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid? id);


    }
}
