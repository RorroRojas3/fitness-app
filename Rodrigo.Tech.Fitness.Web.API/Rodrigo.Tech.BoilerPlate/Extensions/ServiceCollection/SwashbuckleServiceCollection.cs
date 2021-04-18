using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Rodrigo.Tech.Model.Constants;
using Rodrigo.Tech.Model.Settings;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rodrigo.Tech.Fitness.Web.API.Extensions.ServiceCollection
{
    public static class SwashbuckleServiceCollection
    {
        /// <summary>
        ///     Service to add Swashbuckle (Swagger)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSwashbuckleService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc();
            services.AddApiVersioning();
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            var tenantId = Environment.GetEnvironmentVariable(EnvironmentConstants.TENANT_ID);
            var clientId = Environment.GetEnvironmentVariable(EnvironmentConstants.CLIENT_ID);
            var azureAd = configuration.GetSection("AzureAd").Get<AzureAd>();
            services.AddSwaggerGen(x =>
            {
                x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Name = "oauth2",
                    Type = SecuritySchemeType.OAuth2,
                    Description = "JWT Authorization header using OAuth2 Schema",
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            TokenUrl = new Uri($"{azureAd.Instance}/{tenantId}/{azureAd.OAuth2_Token}"),
                            AuthorizationUrl = new Uri($"{azureAd.Instance}/{tenantId}/{azureAd.OAuth2_Authorize}"),
                            Scopes = { { string.Format(azureAd.Scope, clientId), "User access" } }
                        }
                    }
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new [] {"ReadWriteAccess"}
                    }
                });
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
                        Title = $"Doge Fitness API",
                        Description = $"API for Doge Fitness",
                        Version = description.ApiVersion.ToString(),
                    });
            }
        }
    }
}