using AutoMapper;
using Google.Apis.Auth;
using Rodrigo.Tech.Model.Request.V1;
using Rodrigo.Tech.Model.Response.V1;
using Rodrigo.Tech.Repository.Tables.Context;
using System.IO;

namespace Rodrigo.Tech.Model.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            FileMap();
            ExcerciseMap();
            UserMap();
        }

        private void FileMap()
        {
            //CreateMap<File, FileResponse>();
        }

        private void ExcerciseMap()
        {
            CreateMap<ExcerciseRequest, Excercise>();
            CreateMap<Excercise, ExcerciseResponse>();

            CreateMap<ExcerciseTypeIcon, ExcerciseTypeIconResponse>()
                .ForMember(x => x.Stream, opt =>
                    opt.MapFrom(src => new MemoryStream(src.Icon)));
        }

        private void UserMap()
        {
            CreateMap<GoogleJsonWebSignature.Payload, UserResponse>()
                .ForMember(dst => dst.FirstName, opt =>
                    opt.MapFrom(src => src.GivenName))
                .ForMember(dst => dst.LastName, opt =>
                    opt.MapFrom(dst => dst.FamilyName));
            CreateMap<UserResponse, User>();
            CreateMap<User, AuthorizedUserResponse>();
            CreateMap<FacebookUserInformationResponse, UserResponse>()
                .ForMember(dst => dst.Picture, opt =>
                    opt.MapFrom(src => src.Picture.Data.Url));
        }
    }
}