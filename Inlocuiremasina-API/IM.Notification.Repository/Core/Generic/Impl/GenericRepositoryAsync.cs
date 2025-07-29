using IM.Notification.Data.Context;
using IM.Notification.Repository.Core.Generic.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IM.Notification.Repository.Core.Generic.Impl
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly IMNotificationDbCotext _dbContext;

        public GenericRepositoryAsync(IMNotificationDbCotext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> GetByConditionsAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().AsNoTracking().Where(match).ToListAsync();
        }
        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(match);
        }
        public async Task<IReadOnlyList<T>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .AsNoTracking()
                 .ToListAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch(DbUpdateException ex) 
            {
                throw ex;
            }

        }
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(List<T> entity)
        {
            try
            {
                await _dbContext.Set<T>().AddRangeAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: " + ex.InnerException?.Message, ex);
            }
        }

        public async Task UpdateManyAsync(List<T> entities)
        {
            try
            {
                _dbContext.Set<T>().UpdateRange(entities);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: " + ex.InnerException?.Message, ex);
            }
        }

    }
}
