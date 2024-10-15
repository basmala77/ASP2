namespace WebApplication1.Middlewares
{
    public class ProfilingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddleware> _logger;
        public ProfilingMiddleware(RequestDelegate next, ILogger<ProfilingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            var startTime = DateTime.Now;

            await _next(context);

            var endTime = DateTime.Now;
            var duration = endTime - startTime;

            _logger.LogInformation("Request for {Path} took {Duration} ms",
                                    context.Request.Path, duration.TotalMilliseconds);
        }
    }
}
