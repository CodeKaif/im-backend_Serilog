
using ConfigurationModel.CacheSetting;
using IM.CacheService.V1.Interface;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace IM.CacheService.V1.Impl
{
    public class CacheSrvc: ICacheSrvc
    {
        private readonly IDatabase _db;
        private readonly ConfigurationOptions _configOptions;

        private static readonly TimeSpan DefaultExpiry = TimeSpan.FromMinutes(30);

        public CacheSrvc(IOptions<CacheConfigurationSetting> configOptions)
        {
            var config = configOptions.Value;  // Get config from IOptions

            _configOptions = new ConfigurationOptions
            {
                EndPoints = { $"{config.Host}:{config.Port}" },
                Password = config.Password,
                AbortOnConnectFail = false
            };

            var redis = ConnectionMultiplexer.Connect(_configOptions);
            _db = redis.GetDatabase();
        }
        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            try
            {
                var json = JsonSerializer.Serialize(value);
                await _db.StringSetAsync(key, json, expiry ?? DefaultExpiry);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }  
        }
        public async Task<T> GetAsync<T>(string key)
        {
            try
            {
                var json = await _db.StringGetAsync(key);
                return json.HasValue ? JsonSerializer.Deserialize<T>(json!) : default;
            }
            catch (Exception ex)
            {
                return default;
            }
        }
        public async Task<bool> RemoveAsync(string key)
        {
            try
            {
                return await _db.KeyDeleteAsync(key);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> ExistsAsync(string key)
        {
            try
            {
                return await _db.KeyExistsAsync(key);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveByPatternAsync(string pattern)
        {
            try
            {
                var server = _db.Multiplexer.GetServer(_db.Multiplexer.GetEndPoints().First());
                var keys = server.Keys(pattern: pattern).ToArray();

                if (keys.Length > 0)
                {
                    await _db.KeyDeleteAsync(keys);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
