using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Kader_System.Services.AppServices
{
    public class DeflateCompressionAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();

            var response = executedContext.HttpContext.Response;
            var originalBodyStream = response.Body;

            if (response.ContentType != null && response.ContentType.Contains("application/json"))
            {
                using var compressedStream = new MemoryStream();
                using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress))
                {
                    await originalBodyStream.CopyToAsync(gzipStream);
                }

                response.Body = compressedStream;
                response.ContentLength = compressedStream.Length;
                response.Headers.ContentEncoding = "gzip";
                response.ContentType = "application/json";

                await response.Body.WriteAsync(compressedStream.ToArray(), 0, (int)compressedStream.Length);
            }
        }
    }
}