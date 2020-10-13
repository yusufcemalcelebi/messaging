using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Messaging.Api.Helpers
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, env, _logger);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception,
            IWebHostEnvironment env, ILogger _logger)
        {
            _logger.LogError(exception.ToString());

            string result;
            if (env.IsEnvironment("Development"))
                result = JsonSerializer.Serialize(new { error = exception.Message, exception.StackTrace });
            else
                result = JsonSerializer.Serialize(new { error = "An unexpected error occured" });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(result);
        }
    }
}
