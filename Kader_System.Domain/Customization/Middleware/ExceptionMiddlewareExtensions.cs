namespace Kader_System.Domain.Customization.Middleware;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggingRepository loggingRepository)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var ex = contextFeature!.Error;

                    await loggingRepository.LogExceptionInDb(ex, string.Empty);

                    string text = new SelectListForExcLog()
                    {
                        Message = ex.Message,
                        Error = ex.Message + (ex.InnerException == null ? string.Empty : ex.InnerException.Message)
                    }.ToString() ?? string.Empty;

                    await context.Response.WriteAsync(text);
                }
            });
        });
    }
}
