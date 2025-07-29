namespace IM.CacheService.V1.Interface
{
    public interface ICacheSrvc
    {
        Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task<T> GetAsync<T>(string key);
        Task<bool> RemoveAsync(string key);
        Task<bool> ExistsAsync(string key);
        Task<bool> RemoveByPatternAsync(string pattern);
    }
}
