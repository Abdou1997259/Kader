namespace Kader_System.Domain.Customization.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException validationException)
            {
                // Handle validation exceptions
                LogValidationException(validationException);

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var result = JsonConvert.SerializeObject(new Response<object>()
                {
                  Error = validationException.Message,
                  Msg = "Validation Error",
                  Check = false,
                  Data = null,
                  
                });

                await context.Response.WriteAsync(result);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                // Handle unauthorized access exceptions
                LogUnauthorizedAccessException(unauthorizedAccessException);

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";

                var result = JsonConvert.SerializeObject(new
                {
                    error = "Unauthorized access.",
                    message = unauthorizedAccessException.Message
                });

                await context.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                // Log other exceptions
                LogException(ex);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var result = JsonConvert.SerializeObject(new
                {
                    error = "An unexpected error occurred. Please try again later."
                });

                await context.Response.WriteAsync(result);
            }
        }

        private void LogValidationException(ValidationException ex)
        {
            // Log the validation exception using your preferred logging mechanism
            // logger.LogWarning(ex, "Validation failed.");
        }

        private void LogUnauthorizedAccessException(UnauthorizedAccessException ex)
        {
            // Log the unauthorized access exception using your preferred logging mechanism
            // logger.LogWarning(ex, "Unauthorized access.");
        }

        private void LogException(Exception ex)
        {
            // Log other exceptions using your preferred logging mechanism
            // logger.LogError(ex, "An unhandled exception occurred.");
        }
    }
}
