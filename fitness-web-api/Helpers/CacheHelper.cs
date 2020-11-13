using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace fitness_web_api.Helpers
{
    public class CacheHelper
    {
        private readonly IDistributedCache _cache;

        /// <param name="cache"></param>
        public CacheHelper(IDistributedCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        ///     Gets byte array from _cache based on key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<byte[]> GetAsync(string key)
        {
            return await _cache.GetAsync(key);
        }

        /// <summary>
        ///     Sets data in _cache for miliseconds
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task SetDatatMillAsync(string key, byte[] data, int time)
        {
            var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMilliseconds(time));

            await _cache.SetAsync(key, data, options);
        }

        /// <summary>
        ///     Sets data in _cache in seconds
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task SetDatatSecAsync(string key, byte[] data, int time)
        {
            var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(time));

            await _cache.SetAsync(key, data, options);
        }

        /// <summary>
        ///     Sets data in _cache for minutes
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task SetDatatMinAsync(string key, byte[] data, int time)
        {
            var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(time));

            await _cache.SetAsync(key, data, options);
        }

        /// <summary>
        ///     Sets data _cache for hours
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task SetDatatHourAsync(string key, byte[] data, int time)
        {
            var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromHours(time));

            await _cache.SetAsync(key, data, options);
        }

        /// <summary>
        ///     Sets data in _cache for days
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task SetDatatDaysAsync(string key, byte[] data, int time)
        {
            var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromDays(time));

            await _cache.SetAsync(key, data, options);
        }

        /// <summary>
        ///     Removes object from _cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task RemoveCacheAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}