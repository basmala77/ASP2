using Azure.Core;

namespace WebApplication1.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate requestDelegate, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            this.requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Request: {method} {path}", context.Request.Method, context.Request.Path);

            await requestDelegate(context);

            _logger.LogInformation("Response: {statusCode}", context.Response.StatusCode);
        }
    }

}

