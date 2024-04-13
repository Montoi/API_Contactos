using API_Contactos.Models;
using System.Linq.Expressions;

namespace API_Contactos.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task CreateAsync(T entity);
        Task Remove(T entity);
        Task SaveAsync();
    }
}
