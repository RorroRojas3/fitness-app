using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Model.AutoMapper;

namespace Rodrigo.Tech.Fitness.Web.API.Extensions.ServiceCollection
{
    public static class AutoMapperServiceCollection
    {
        /// <summary>
        ///     Adds AutoMapper service
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }
    }
}