using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rodrigo.Tech.Model.Requests;
using Rodrigo.Tech.Model.Response;
using Rodrigo.Tech.Respository.Pattern.Interface;
using Rodrigo.Tech.Respository.Tables.Context;
using Rodrigo.Tech.Service.Helpers;
using Rodrigo.Tech.Service.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Service.Implementation
{
    public class ItemService : IItemService
    {
        private readonly IRepository<Item> _itemRepository;
        private readonly ICacheService _cacheService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ItemService(IRepository<Item> itemRepository,
                            ICacheService cacheHelper,
                            ILogger<ItemService> logger,
                            IMapper mapper)
        {
            _itemRepository = itemRepository;
            _cacheService = cacheHelper;
            _logger = logger;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<bool>> DeleteItem(Guid id)
        {
            _logger.LogInformation($"{nameof(ItemService)} - {nameof(DeleteItem)} - Started, " +
                $"{nameof(id)}: {id}");

            var isDeleted =  await _itemRepository.Delete(id);

            if (!isDeleted)
            {
                return new ApiResponse<bool>(HttpStatusCode.NotFound, isDeleted, $"item not found");
            }

            _logger.LogInformation($"{nameof(ItemService)} - {nameof(DeleteItem)} - Finished, " +
                $"{nameof(id)}: {id}");
            return new ApiResponse<bool>(HttpStatusCode.OK, true);
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<ItemResponse>> GetItem(Guid id)
        {
            _logger.LogInformation($"{nameof(ItemService)} - {nameof(GetItem)} - Started, " +
                $"{nameof(id)}: {id}");

            var cacheBytes = await _cacheService.GetAsync($"item-{id}");

            if (cacheBytes != null)
            {
                var cacheItem = SerializeHelper.DeserializeObject<Item>(cacheBytes);
                var itemResponse = _mapper.Map<ItemResponse>(cacheItem);
                return new ApiResponse<ItemResponse>(HttpStatusCode.OK, itemResponse);
            }

            var item = await _itemRepository.Get(id);

            if (item == null)
            {
                _logger.LogError($"{nameof(ItemService)} - {nameof(GetItem)} - Not found, " +
                $"{nameof(id)}: {id}");
                return new ApiResponse<ItemResponse>(HttpStatusCode.NotFound);
            }

            var serializedItem = SerializeHelper.SerializeObject(item);

            await _cacheService.SetDataMinAsync($"item-{id}", serializedItem, 5);

            var mappedItem = _mapper.Map<ItemResponse>(item);

            _logger.LogInformation($"{nameof(ItemService)} - {nameof(GetItem)} - Finished, " +
                $"{nameof(id)}: {id}");
            return new ApiResponse<ItemResponse>(HttpStatusCode.OK, mappedItem);
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<IList<ItemResponse>>> GetItems()
        {
            _logger.LogInformation($"{nameof(ItemService)} - {nameof(GetItems)} - Started");

            var cacheBytes = await _cacheService.GetAsync($"items");

            if (cacheBytes != null)
            {
                var cacheItems = SerializeHelper.DeserializeObject<List<Item>>(cacheBytes);
                var itemResponses = _mapper.Map<List<ItemResponse>>(cacheItems);
                return new ApiResponse<IList<ItemResponse>>(HttpStatusCode.OK, itemResponses);
            }

            var items = await _itemRepository.GetAll();

            if (items.Count == 0)
            {
                _logger.LogError($"{nameof(ItemService)} - {nameof(GetItems)} - Not found");
                return new ApiResponse<IList<ItemResponse>>(HttpStatusCode.NotFound);
            }

            var serializedItem = SerializeHelper.SerializeObject(items);

            await _cacheService.SetDataMinAsync($"items", serializedItem, 5);

            var mappedItems =  _mapper.Map<List<ItemResponse>>(items);

            _logger.LogInformation($"{nameof(ItemService)} - {nameof(GetItems)} - Finished");
            return new ApiResponse<IList<ItemResponse>>(HttpStatusCode.OK, mappedItems);
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<ItemResponse>> PostItem(ItemRequest request)
        {
            _logger.LogInformation($"{nameof(ItemService)} - {nameof(PostItem)} - Started, " +
                $"{nameof(ItemRequest)}: {JsonConvert.SerializeObject(request)}");

            var newItem = _mapper.Map<Item>(request);

            var result =  await _itemRepository.Add(newItem);

            var mappedItem = _mapper.Map<ItemResponse>(result);

            _logger.LogInformation($"{nameof(ItemService)} - {nameof(PostItem)} - Finished, " +
                $"{nameof(ItemRequest)}: {JsonConvert.SerializeObject(request)}");
            return new ApiResponse<ItemResponse>(HttpStatusCode.Created, mappedItem);
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<ItemResponse>> PutItem(Guid id, ItemRequest request)
        {
            _logger.LogInformation($"{nameof(ItemService)} - {nameof(PutItem)} - Started, " +
                $"{nameof(id)}: {id}, " +
                $"{nameof(ItemRequest)}: {JsonConvert.SerializeObject(request)}");

            var item = await _itemRepository.Get(id);

            if (item == null)
            {
                _logger.LogError($"{nameof(ItemService)} - {nameof(PutItem)} - Not found, " +
                $"{nameof(id)}: {id}, " +
                $"{nameof(ItemRequest)}: {JsonConvert.SerializeObject(request)}");
                return new ApiResponse<ItemResponse>(HttpStatusCode.NotFound);
            }

            await _cacheService.RemoveCacheAsync($"item-{id}");

            _mapper.Map(request, item);

            var result =  await _itemRepository.Update(item);

            var mappedItem = _mapper.Map<ItemResponse>(result);

            _logger.LogInformation($"{nameof(ItemService)} - {nameof(PutItem)} - Finished, " +
                $"{nameof(id)}: {id}, " +
                $"{nameof(ItemRequest)}: {JsonConvert.SerializeObject(request)}");
            return new ApiResponse<ItemResponse>(HttpStatusCode.OK, mappedItem);
        }
    }
}