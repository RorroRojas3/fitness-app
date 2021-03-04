using AutoMapper;
using Rodrigo.Tech.Model.Request.V1;
using Rodrigo.Tech.Model.Response.V1;
using Rodrigo.Tech.Repository.Tables.Context;

namespace Rodrigo.Tech.Model.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            FileMap();
            ExcerciseMap();
        }

        private void FileMap()
        {
            CreateMap<File, FileResponse>();
        }

        private void ExcerciseMap()
        {
            CreateMap<ExcerciseRequest, Excercise>();
            CreateMap<Excercise, ExcerciseResponse>();
        }
    }
}