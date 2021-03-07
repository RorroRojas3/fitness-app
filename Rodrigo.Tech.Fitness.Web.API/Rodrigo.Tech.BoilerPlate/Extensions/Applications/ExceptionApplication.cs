using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rodrigo.Tech.Model.Exceptions;

namespace Rodrigo.Tech.Fitness.Web.API.Extensions.Applications
{
    public static class ExceptionApplication
    {
        /// <summary>
        ///     Adds Exception middleware
        /// </summary>
        /// <param name="application"></param>
        public static void UseExceptionMiddleware(this IApplicationBuilder application)
        {
            application.UseMiddleware<ExceptionMiddleware>();
        }
    }

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        ///     Invokes exception on API call
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"StackTrace: {ex.StackTrace}, Exception: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        ///     Handles exceptions
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string errorMessage = "Internal server error";

            if (exception is StatusCodeException statusCodeException)
            {
                statusCode = (int)statusCodeException.HttpStatusCode;
                errorMessage = statusCodeException.Message;
            }

            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = errorMessage
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}