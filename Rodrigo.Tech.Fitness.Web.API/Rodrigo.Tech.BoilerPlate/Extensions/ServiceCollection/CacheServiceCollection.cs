using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Service.Implementation;
using Rodrigo.Tech.Service.Interface;

namespace Rodrigo.Tech.Fitness.Web.API.Extensions.ServiceCollection
{
    public static class CacheServiceCollection
    {
        /// <summary>
        ///     Adds Distributed cache
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCacheService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICacheService, CacheService>();
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = configuration.GetConnectionString("AZURE_DB");
                options.SchemaName = "dbo";
                options.TableName = "Cache";
            });
        }
    }
}