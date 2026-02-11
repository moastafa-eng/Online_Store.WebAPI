using Domain.Contracts;
using Services.Abstractions.Cache;

namespace Services.Cache
{
    public class CacheService(ICacheRepository _cachRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string key)
        {
            return await _cachRepository.GetAsync(key);
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            await _cachRepository.SetAsync(key, value, duration);
        }
    }
}
