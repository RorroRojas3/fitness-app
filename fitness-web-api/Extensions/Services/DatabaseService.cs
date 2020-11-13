using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using fitness_web_api.Database.DB;
using fitness_web_api.Database.Repository.Implementation;
using fitness_web_api.Database.Repository.Interface;

namespace fitness_web_api.Extensions.Services
{
    public static class DatabaseService
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