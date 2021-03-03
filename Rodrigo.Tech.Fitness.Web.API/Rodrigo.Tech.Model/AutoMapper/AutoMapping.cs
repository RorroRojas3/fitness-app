using AutoMapper;
using Rodrigo.Tech.Model.Requests;
using Rodrigo.Tech.Model.Response;
using Rodrigo.Tech.Respository.Tables.Context;
using Rodrigo.Tech.Respository.Tables.Cosmos;

namespace Rodrigo.Tech.Model.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<File, FileResponse>();
            CreateMap<ItemRequest, ItemCosmos>();
            CreateMap<ItemRequest, Item>();
            CreateMap<Item, ItemResponse>();
        }
    }
}