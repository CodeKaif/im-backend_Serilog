using System.Linq.Expressions;

namespace IM.Notification.Repository.Core.Generic.Interface
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetByConditionsAsync(Expression<Func<T, bool>> match);
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> match);
        Task<IReadOnlyList<T>> GetPagedResponseAsync(int pageNumber, int pageSize);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task AddRangeAsync(List<T> entity);
        Task UpdateManyAsync(List<T> entities);
    }
}
