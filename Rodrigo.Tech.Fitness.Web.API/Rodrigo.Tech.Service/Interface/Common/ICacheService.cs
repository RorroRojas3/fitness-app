using System.Threading.Tasks;

namespace Rodrigo.Tech.Service.Interface.Common
{
    public interface ICacheService
    {
        /// <summary>
        ///     Gets byte array from _cache based on key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<byte[]> GetAsync(string key);

        /// <summary>
        ///     Sets data in _cache for miliseconds
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task SetDataMillAsync(string key, byte[] data, int time);

        /// <summary>
        ///     Sets data in _cache in seconds
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task SetDataSecAsync(string key, byte[] data, int time);

        /// <summary>
        ///     Sets data in _cache for minutes
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task SetDataMinAsync(string key, byte[] data, int time);

        /// <summary>
        ///     Sets data _cache for hours
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task SetDataHourAsync(string key, byte[] data, int time);

        /// <summary>
        ///     Sets data in _cache for days
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task SetDataDaysAsync(string key, byte[] data, int time);

        /// <summary>
        ///     Removes object from _cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveCacheAsync(string key);
    }
}
