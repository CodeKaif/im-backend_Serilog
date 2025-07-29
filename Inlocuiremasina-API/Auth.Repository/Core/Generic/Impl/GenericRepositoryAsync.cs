using Auth.Data.Context;
using Auth.Repository.Core.Generic.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Auth.Repository.Core.Generic.Impl
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly IMAuthDbContext _dbContext;

        public GenericRepositoryAsync(IMAuthDbContext dbContext)
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
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
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
