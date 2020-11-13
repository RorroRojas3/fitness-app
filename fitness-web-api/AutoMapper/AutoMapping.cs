using AutoMapper;
using fitness_web_api.Database.Tables;
using fitness_web_api.Models.Responses;

namespace fitness_web_api.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<File, FileResponse>();
        }
    }
}