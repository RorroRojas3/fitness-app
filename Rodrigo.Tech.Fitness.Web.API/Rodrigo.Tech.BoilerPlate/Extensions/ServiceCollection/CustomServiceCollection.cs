using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Service.Implementation;
using Rodrigo.Tech.Service.Interface.V1;

namespace Rodrigo.Tech.Fitness.Web.API.Extensions.ServiceCollection
{
    public static class CustomServiceCollection
    {
        /// <summary>
        ///     Adds service for TestController
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IExcerciseService, ExcerciseService>();
        }
    }
}