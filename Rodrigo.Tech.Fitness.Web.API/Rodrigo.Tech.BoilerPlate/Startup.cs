using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Fitness.Web.API.Extensions.Applications;
using Rodrigo.Tech.Fitness.Web.API.Extensions.ServiceCollection;
using Rodrigo.Tech.Respository.Context;
using Serilog;

namespace Rodrigo.Tech.Fitness.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        ///     Configure services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLoggingService(Configuration);
            Log.Information("Adding Swashbuckle Service");
            services.AddSwashbuckleService(Configuration);
            Log.Information("Addding Authentication Service");
            services.AddAuthenticationService(Configuration);
            Log.Information("Adding Controller and NewtonSoftJson Service");
            services.AddControllers().AddNewtonsoftJson();
            Log.Information("Adding Custom Service");
            services.AddCustomService();
            Log.Information("Adding Database Service");
            services.AddDatabaseService(Configuration);
            Log.Information("Adding AutoMapper service");
            services.AddAutoMapperService();
            Log.Information("Adding Cache Service");
            services.AddCacheService(Configuration);
            Log.Information("Adding Data Protection Service");
            services.AddDataProtectionService();
            Log.Information("Adding Health Checks");
            services.AddCustomHealthChecks(Configuration);
            Log.Information("Adding HttpContextAccesor Service");
            services.AddHttpContextAccessor();
        }

        /// <summary>
        ///     Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        /// <param name="db"></param>
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider,
                                DatabaseContext db)
        {
            Log.Information($"Using Exception middleware");
            app.UseExceptionMiddleware();
            Log.Information($"Using HttpsRedirection");
            app.UseHttpsRedirection();
            Log.Information($"Using Authentication");
            app.UseAuthentication();
            Log.Information($"Using Routing");
            app.UseRouting();
            Log.Information($"Using Auhtorization");
            app.UseAuthorization();
            Log.Information($"Using Endpoint");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI();
            });
            Log.Information($"Using Swashbuckle");
            app.UseSwashbuckle(provider);

            Log.Information($"Using Migration of DB");
            db.Database.Migrate();
        }
    }
}
