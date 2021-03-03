using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Rodrigo.Tech.Model.Constants;
using System;

namespace Rodrigo.Tech.Fitness.Web.API.Extensions.Applications
{
    public static class SwashbuckleApplication
    {
        /// <summary>
        ///     Adds Swashbuckle application 
        /// </summary>
        /// <param name="application"></param>
        /// <param name="provider"></param>
        public static void UseSwashbuckle(this IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseSwagger();
            application.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }

                    options.RoutePrefix = string.Empty;
                    options.OAuthClientId(Environment.GetEnvironmentVariable(EnvironmentConstants.CLIENT_ID));
                    options.OAuthAppName("ASP.NET Core Web Api");
                });
        }
    }
}