using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using fitness_web_api.Services.Implementation;

namespace fitness_web_api.Extensions.Services
{
    public static class AutoMapperService
    {
        /// <summary>
        ///     Adds AutoMapper service
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }
    }
}