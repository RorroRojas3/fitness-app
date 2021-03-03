using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Respository.Context;
using Rodrigo.Tech.Respository.Pattern.Implementation;
using Rodrigo.Tech.Respository.Pattern.Interface;

namespace Rodrigo.Tech.Fitness.Web.API.Extensions.ServiceCollection
{
    public static class DatabaseServiceCollection
    {
        /// <summary>
        ///     Adds database service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("AZURE_DB")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}