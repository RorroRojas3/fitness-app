using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace fitness_web_api.Extensions.Services
{
    public static class SwashbuckleService
    {
        /// <summary>
        ///     Service to add Swashbuckle (Swagger)
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwashbuckleService(this IServiceCollection services)
        {
            services.AddMvc();
            services.AddApiVersioning();
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(x =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });
        }
    }

    /// <summary>
    ///     ConfigureSwaggerOptions class
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        /// <summary>
        ///     Private variables
        /// </summary>
        readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        ///     ConfigureSwaggerOptions constructor with DI
        /// </summary>
        /// <param name="provider"></param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
          this.provider = provider;

        /// <summary>
        ///     Adds swagger documentation
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                  description.GroupName,
                    new OpenApiInfo()
                    {
                        Title = $".NET Core Boiler Plate",
                        Description = $".NET Core Boiler Plate Example",
                        Version = description.ApiVersion.ToString(),
                    });
            }
        }
    }
}