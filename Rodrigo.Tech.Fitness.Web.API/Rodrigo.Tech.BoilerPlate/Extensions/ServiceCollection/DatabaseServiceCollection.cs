using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Repository.Pattern.Implementation;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Respository.Context;

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
            services.AddDbContext<FitnessDatabase>(options => options.UseSqlServer(configuration.GetConnectionString("AZURE_DB")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}