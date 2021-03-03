using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Respository.Pattern.Interface
{
    public interface ICosmosRepository
    {
        /// <summary>
        ///     Gets all items
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="containerName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetItemsAsync<T>(string dataBaseName, string containerName);

        /// <summary>
        ///     Gets single item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="containerName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetItemAsync<T>(string id, string dataBaseName, string containerName);

        /// <summary>
        ///     Adds item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="containerName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task AddItemAsync<T>(T item, string id, string dataBaseName, string containerName);

        /// <summary>
        ///     Updates an item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="containerName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task UpdateItemAsync<T>(string id, T item, string dataBaseName, string containerName);

        /// <summary>
        ///     Deletes an Item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task DeleteItemAsync(string id, string dataBaseName, string containerName);

        /// <summary>
        ///     Gets item based on property
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="containerName"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetItemByProperty<T>(string dataBaseName, string containerName, Expression<Func<T, bool>> predicate);
    }
}