using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using fitness_web_api.Database.Repository.Interface;
using fitness_web_api.Database.Tables;
using fitness_web_api.Helpers;
using fitness_web_api.Models.Requests;
using fitness_web_api.Services.Interface;

namespace fitness_web_api.Services.Implementation
{
    public class ItemService : IItemService
    {
        private readonly IRepository<Item> _itemRepository;
        private readonly CacheHelper _cacheHelper;

        public ItemService(IRepository<Item> itemRepository,
                            CacheHelper cacheHelper)
        {
            _itemRepository = itemRepository;
            _cacheHelper = cacheHelper;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteItem(Guid id)
        {
            return await _itemRepository.Delete(id);
        }

        /// <inheritdoc/>
        public async Task<Item> GetItem(Guid id)
        {
            var cacheBytes = await _cacheHelper.GetAsync($"item-{id}");

            if (cacheBytes != null)
            {
                var cacheItem = SerializeHelper.DeserializeObject<Item>(cacheBytes);
                return cacheItem;
            }

            var item = await _itemRepository.Get(id);

            if (item == null)
            {
                return null;
            }

            var serializedItem = SerializeHelper.SerializeObject(item);

            await _cacheHelper.SetDatatMinAsync($"item-{id}", serializedItem, 5);

            return item;
        }

        /// <inheritdoc/>
        public async Task<List<Item>> GetItems()
        {
            var cacheBytes = await _cacheHelper.GetAsync($"items");

            if (cacheBytes != null)
            {
                var cacheItem = SerializeHelper.DeserializeObject<List<Item>>(cacheBytes);
                return cacheItem;
            }

            var items = await _itemRepository.GetAll();

            if (items == null)
            {
                return null;
            }

            var serializedItem = SerializeHelper.SerializeObject(items);

            await _cacheHelper.SetDatatMinAsync($"items", serializedItem, 5);

            return items;
        }

        /// <inheritdoc/>
        public async Task<Item> PostItem(ItemRequest item)
        {
            var newItem = new Item
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Value = item.Value
            };

            return await _itemRepository.Add(newItem);
        }

        /// <inheritdoc/>
        public async Task<Item> PutItem(Guid id, ItemRequest itemRequest)
        {
            var item = await GetItem(id);

            if (item == null)
            {
                return null;
            }

            await _cacheHelper.RemoveCacheAsync($"item-{id}");

            item.Name = itemRequest.Name;
            item.Value = itemRequest.Value;

            return await _itemRepository.Update(item);
        }
    }
}