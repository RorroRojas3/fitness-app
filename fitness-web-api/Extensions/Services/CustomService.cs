using Microsoft.Extensions.DependencyInjection;
using fitness_web_api.Services.Implementation;
using fitness_web_api.Services.Interface;

namespace fitness_web_api.Extensions.Services
{
    public static class CustomService
    {
        /// <summary>
        ///     Adds service for TestController
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
        }

    }
}