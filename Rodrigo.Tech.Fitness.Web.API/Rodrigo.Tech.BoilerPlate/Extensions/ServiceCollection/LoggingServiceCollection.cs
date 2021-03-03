using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rodrigo.Tech.Model.Constants;
using Serilog;
using Serilog.Events;

namespace Rodrigo.Tech.Fitness.Web.API.Extensions.ServiceCollection
{
    public static class LoggingServiceCollection
    {
        /// <summary>
        ///     Adds Logging service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddLoggingService(this IServiceCollection services, IConfiguration configuration)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var logDirectory = Path.Combine(currentDirectory, "Logs");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            var instrumentationKey = Environment.GetEnvironmentVariable(EnvironmentConstants.APPINSIGHTS_INSTRUMENTATIONKEY);
            var logger = new LoggerConfiguration()
                            .WriteTo.Console(LogEventLevel.Information)
                            .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces, LogEventLevel.Information)
                            .ReadFrom.Configuration(configuration)
                            .CreateLogger();
            services.AddSingleton(logger);
            services.AddLogging(l => l.AddSerilog(logger));

            Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console(LogEventLevel.Information)
                        .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces, LogEventLevel.Information)
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();
            services.AddSingleton(Log.Logger);
        }
    }
}