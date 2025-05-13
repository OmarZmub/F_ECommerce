using System.Linq.Expressions;

namespace F_ECommerce.Infrastructure.Repositories.Abstractions;

public interface IGenericRepositry<T> where T : class
{
   Task<IReadOnlyList<T>> GetAllAsync();
   Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] Includes);
   Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes);
   Task<T> GetByIdAsync(long id);
   Task AddAsync(T entity);
   Task UpdateAsync(T entity);
   Task DeleteAsync(long id);
   Task<int> CountAsync();

}

