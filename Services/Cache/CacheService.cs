using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Books.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public bool Exists(string key)
        {
            return _memoryCache.TryGetValue(key, out object _);
        }

        public T Get<T>(string key)
        {
            var json = _memoryCache.Get<string>(key);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Save<T>(string key, T payload)
        {
            var jsonValue = JsonConvert.SerializeObject(payload);
            _memoryCache.Set<string>(key, jsonValue);
        }
    }
}
