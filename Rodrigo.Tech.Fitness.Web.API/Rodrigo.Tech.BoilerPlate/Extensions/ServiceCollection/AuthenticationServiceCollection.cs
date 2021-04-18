using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Rodrigo.Tech.Model.Constants;
using Rodrigo.Tech.Model.Settings;
using System;

namespace Rodrigo.Tech.Fitness.Web.API.Extensions.ServiceCollection
{
    public static class AuthenticationServiceCollection
    {
        /// <summary>
        ///     Adds Authentication Service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            var tenantId = Environment.GetEnvironmentVariable(EnvironmentConstants.TENANT_ID);
            var clientId = Environment.GetEnvironmentVariable(EnvironmentConstants.CLIENT_ID);
            var azureAd = configuration.GetSection("AzureAd").Get<AzureAd>();
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Audience = string.Format(azureAd.Audience, clientId);
                options.Authority = $"{azureAd.Instance}/{tenantId}";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true
                };
            });
        }
    }
}