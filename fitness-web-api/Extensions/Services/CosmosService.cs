using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using fitness_web_api.Database.Repository.Implementation;
using fitness_web_api.Database.Repository.Interface;
using fitness_web_api.Models.Settings;
using fitness_web_api.Services.Implementation;
using fitness_web_api.Services.Interface;

namespace fitness_web_api.Extensions.Services
{
    public static class CosmosService
    {
        /// <summary>
        ///     Adds Azure Cosmos DB
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddAzureCosmosService(this IServiceCollection services, IConfiguration configuration)
        {
            var cosmosDb = configuration.GetSection("CosmosDb").Get<CosmosDb>();
            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(cosmosDb.Account, cosmosDb.Key);
            CosmosClient client = clientBuilder
                                .WithConnectionModeDirect()
                                .Build();

            DatabaseResponse database = client.CreateDatabaseIfNotExistsAsync(cosmosDb.DatabaseName).GetAwaiter().GetResult();

            foreach (var item in cosmosDb.ContainerCollection)
            {
                database.Database.CreateContainerIfNotExistsAsync(item.Name, item.PartitionKey);
            }
            
            services.AddSingleton(client);
            services.AddScoped<ICosmosRepository, CosmosRepository>();
            services.AddSingleton(cosmosDb);
            services.AddScoped<IItemCosmosService, ItemCosmosService>();
        }
    }
}