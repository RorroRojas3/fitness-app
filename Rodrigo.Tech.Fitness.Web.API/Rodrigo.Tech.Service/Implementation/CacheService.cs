using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Rodrigo.Tech.Service.Interface;

namespace Rodrigo.Tech.Service.Implementation
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;

        public CacheService(IDistributedCache cache,
                            ILogger<CacheService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<byte[]> GetAsync(string key)
        {
            _logger.LogInformation($"{nameof(CacheService)} - {nameof(GetAsync)} - Started, " +
                $"{nameof(key)}: {key}");

            var result = await _cache.GetAsync(key);

            _logger.LogInformation($"{nameof(CacheService)} - {nameof(GetAsync)} - Started, " +
                $"{nameof(key)}: {key}");
            return result;
        }

        /// <inheritdoc/>
        public async Task SetDataMillAsync(string key, byte[] data, int time)
        {
            _logger.LogInformation($"{nameof(CacheService)} - {nameof(SetDataMillAsync)} - Started, " +
                $"{nameof(key)}: {key}, " +
                $"{nameof(time)}: {time}");

            var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMilliseconds(time));

            await _cache.SetAsync(key, data, options);

            _logger.LogInformation($"{nameof(CacheService)} - {nameof(SetDataMillAsync)} - Finished, " +
                $"{nameof(key)}: {key}, " +
                $"{nameof(time)}: {time}");
        }

        /// <inheritdoc/>
        public async Task SetDataSecAsync(string key, byte[] data, int time)
        {
            _logger.LogInformation($"{nameof(CacheService)} - {nameof(SetDataSecAsync)} - Started, " +
                $"{nameof(key)}: {key}, " +
                $"{nameof(time)}: {time}");

            var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(time));

            await _cache.SetAsync(key, data, options);

            _logger.LogInformation($"{nameof(CacheService)} - {nameof(SetDataSecAsync)} - Finished, " +
                $"{nameof(key)}: {key}, " +
                $"{nameof(time)}: {time}");
        }

        /// <inheritdoc/>
        public async Task SetDataMinAsync(string key, byte[] data, int time)
        {
            _logger.LogInformation($"{nameof(CacheService)} - {nameof(SetDataMinAsync)} - Started, " +
                $"{nameof(key)}: {key}, " +
                $"{nameof(time)}: {time}");

            var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(time));

            await _cache.SetAsync(key, data, options);

            _logger.LogInformation($"{nameof(CacheService)} - {nameof(SetDataMinAsync)} - Finished, " +
                $"{nameof(key)}: {key}, " +
                $"{nameof(time)}: {time}");
        }

        /// <inheritdoc/>
        public async Task SetDataHourAsync(string key, byte[] data, int time)
        {
            _logger.LogInformation($"{nameof(CacheService)} - {nameof(SetDataHourAsync)} - Started, " +
                $"{nameof(key)}: {key}, " +
                $"{nameof(time)}: {time}");

            var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromHours(time));

            await _cache.SetAsync(key, data, options);

            _logger.LogInformation($"{nameof(CacheService)} - {nameof(SetDataHourAsync)} - Finished, " +
                $"{nameof(key)}: {key}, " +
                $"{nameof(time)}: {time}");
        }

        /// <inheritdoc/>
        public async Task SetDataDaysAsync(string key, byte[] data, int time)
        {
            _logger.LogInformation($"{nameof(CacheService)} - {nameof(SetDataDaysAsync)} - Started, " +
                $"{nameof(key)}: {key}, " +
                $"{nameof(time)}: {time}");

            var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromDays(time));

            await _cache.SetAsync(key, data, options);

            _logger.LogInformation($"{nameof(CacheService)} - {nameof(SetDataDaysAsync)} - Finished, " +
                $"{nameof(key)}: {key}, " +
                $"{nameof(time)}: {time}");
        }

        /// <inheritdoc/>
        public async Task RemoveCacheAsync(string key)
        {
            _logger.LogInformation($"{nameof(CacheService)} - {nameof(RemoveCacheAsync)} - Started, " +
                $"{nameof(key)}: {key}");
            await _cache.RemoveAsync(key);

            _logger.LogInformation($"{nameof(CacheService)} - {nameof(RemoveCacheAsync)} - Finished, " +
                $"{nameof(key)}: {key}");
        }
    }
}