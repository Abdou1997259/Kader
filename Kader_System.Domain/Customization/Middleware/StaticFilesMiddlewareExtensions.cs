namespace Kader_System.Domain.Customization.Middleware;

public static class StaticFilesMiddlewareExtensions
{
    public static void ConfigureStaticFilesHandler(this IApplicationBuilder app)
    {
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "FilesServer")),
            RequestPath = new PathString("/FilesServer")
        });
    }
}
