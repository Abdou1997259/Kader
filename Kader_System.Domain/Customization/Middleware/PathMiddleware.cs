using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Kader_System.Domain.Customization.Middleware
{
    public class PathMiddleware
    {
        private readonly RequestDelegate _next;

        public PathMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("client_id", out var clientId))
            {
                var wwwrootPath = context.RequestServices.GetRequiredService<IWebHostEnvironment>().WebRootPath;
                var fullPath = Path.Combine(wwwrootPath, clientId);

                // Store the full path in a globally accessible place
                context.Items["ServerPath"] = fullPath;
            }

            await _next(context);
        }
    }
}
