namespace Kader_System.Domain.Customization.Middleware
{
    public class HeadersValidationMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("client_id", out var client_id) || string.IsNullOrEmpty(client_id))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Missing required headers");
                return;
            }
            await _next(context);
        }
    }
}
