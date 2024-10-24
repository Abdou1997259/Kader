namespace Kader_System.Domain.Customization.Middleware
{
    public static class LocalizationMiddleWaresExtensions
    {
        public static void LocalizationMiddleWaresHandler(this IApplicationBuilder builder)
        {
            builder.Use(async (context, next) =>
            {
                var current = context.Request.Headers["Accept-Language"].ToString();
                if (!string.IsNullOrWhiteSpace(current))
                {
                    var cultures = current.Split(',');
                    var selectedCulture = cultures.FirstOrDefault()?.Trim() ?? "en"; // Default to "en"
                    var cultureInfo = new CultureInfo(selectedCulture);

                    // Set the culture for the current thread
                    Thread.CurrentThread.CurrentCulture = cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                }
                await next();
            });
        }
    }
}
