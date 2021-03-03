using Microsoft.Azure.Cosmos;
using Rodrigo.Tech.Respository.Pattern.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Respository.Pattern.Implementation
{
    public class CosmosRepository : ICosmosRepository
    {
        private readonly CosmosClient _cosmosClient;

        public CosmosRepository(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        /// <inheritdoc/>
        public async Task<T> GetItemAsync<T>(string id, string dataBaseName, string containerName)
        {
            var container = GetContainer(dataBaseName, containerName);
            ItemResponse<T> response = await container.ReadItemAsync<T>(id, new PartitionKey(id));
            return response.Resource;       
        }

        /// <inheritdoc/>
        public IEnumerable<T> GetItemsAsync<T>(string dataBaseName, string containerName)
        {
            var container = GetContainer(dataBaseName, containerName);
            var results = container.GetItemLinqQueryable<T>(true).ToList();
            return results;
        }

        /// <inheritdoc/>
        public T GetItemByProperty<T>(string dataBaseName, string containerName, Expression<Func<T, bool>> predicate)
        {
            var container = GetContainer(dataBaseName, containerName);
            var result = container.GetItemLinqQueryable<T>(true).Where(predicate).AsEnumerable().FirstOrDefault();
            return result;
        }

        /// <inheritdoc/>
        public async Task AddItemAsync<T>(T item, string id, string dataBaseName, string containerName)
        {
            var container = GetContainer(dataBaseName, containerName);
            await container.CreateItemAsync(item, new PartitionKey(id));
        }

        /// <inheritdoc/>
        public async Task DeleteItemAsync(string id, string dataBaseName, string containerName)
        {
            var container = GetContainer(dataBaseName, containerName);
            await container.DeleteItemAsync<IEntity>(id, new PartitionKey(id));
        }

        /// <inheritdoc/>
        public async Task UpdateItemAsync<T>(string id, T item, string dataBaseName, string containerName)
        {
            var container = GetContainer(dataBaseName, containerName);
            await container.UpsertItemAsync(item, new PartitionKey(id));
        }

        /// <inheritdoc/>
        private Container GetContainer(string databaseName, string containerName)
        {
            return _cosmosClient.GetContainer(databaseName, containerName);
        }
    }
}