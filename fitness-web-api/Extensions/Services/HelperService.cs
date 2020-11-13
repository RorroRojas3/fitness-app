using Microsoft.Extensions.DependencyInjection;
using fitness_web_api.Helpers;

namespace fitness_web_api.Extensions.Services
{
    public static class HelperService
    {
        /// <summary>
        ///     Adds Helpers
        /// </summary>
        /// <param name="services"></param>
        public static void AddHelpersService(this IServiceCollection services)
        {
            services.AddScoped<CacheHelper>();
        }
    }
}