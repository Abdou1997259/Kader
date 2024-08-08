using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using System.IO.Compression;
using System.Web.Http.Filters;
using ActionFilterAttribute = Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute;

namespace Kader_System.Services.AppServices
{
    /// <summary>
    /// Attribute for enabling Brotli/GZip/Deflate compression for specied action
    /// </summary>
    public class DeflateCompressionAttribute : ActionFilterAttribute
    {
        private Stream _originStream = null;
        private MemoryStream _ms = null;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            HttpRequest request = context.HttpContext.Request;
            string acceptEncoding = request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(acceptEncoding)) return;
            acceptEncoding = acceptEncoding.ToUpperInvariant();
            HttpResponse response = context.HttpContext.Response;
            if (acceptEncoding.Contains("BR", StringComparison.OrdinalIgnoreCase))//Brotli 
            {
                if (!(response.Body is BrotliStream))// avoid twice compression.
                {
                    _originStream = response.Body;
                    _ms = new MemoryStream();
                    response.Headers.Add("Content-encoding", "br");
                    response.Body = new BrotliStream(_ms, CompressionLevel.Optimal);
                }
            }
            else if (acceptEncoding.Contains("GZIP", StringComparison.OrdinalIgnoreCase))
            {
                if (!(response.Body is GZipStream))
                {
                    _originStream = response.Body;
                    _ms = new MemoryStream();
                    response.Headers.Add("Content-Encoding", "gzip");
                    response.Body = new GZipStream(_ms, CompressionLevel.Optimal);
                }
            }
            else if (acceptEncoding.Contains("DEFLATE", StringComparison.OrdinalIgnoreCase))
            {
                if (!(response.Body is DeflateStream))
                {
                    _originStream = response.Body;
                    _ms = new MemoryStream();
                    response.Headers.Add("Content-encoding", "deflate");
                    response.Body = new DeflateStream(_ms, CompressionLevel.Optimal);
                }
            }
            base.OnActionExecuting(context);
        }

        public override async void OnResultExecuted(ResultExecutedContext context)
        {
            if ((_originStream != null) && (_ms != null))
            {
                HttpResponse response = context.HttpContext.Response;
                await response.Body.FlushAsync();
                _ms.Seek(0, SeekOrigin.Begin);
                response.Headers.ContentLength = _ms.Length;
                await _ms.CopyToAsync(_originStream);
                response.Body.Dispose();
                _ms.Dispose();
                response.Body = _originStream;
            }
            base.OnResultExecuted(context);
        }
    }
    //public class DeflateCompressionAttribute : ActionFilterAttribute
    //{
    //    public override async  void  OnActionExecuted(ActionExecutedContext context)
    //    {
    //        var response = context.HttpContext.Response;
    //        var originalBodyStream = response.Body;

    //        using (var memoryStream = new MemoryStream())
    //        {
    //            // Replace the response body stream with a memory stream to capture the content
    //            response.Body = memoryStream;

    //            // Execute the action and await the result
    //            var executedContext = await next();

    //            // Check if the response was successful
    //            if (response.StatusCode == 200 && response.Body.Length > 0)
    //            {
    //                // Compress the captured content
    //                var compressedContent = CompressionHelper.DeflateByte(memoryStream.ToArray());

    //                // Modify the response headers before writing the compressed content
    //                response.Headers["Content-Encoding"] = "gzip";
    //                response.ContentLength = compressedContent.Length;

    //                // Write the compressed content to the original response stream
    //                await originalBodyStream.WriteAsync(compressedContent, 0, compressedContent.Length);
    //            }

    //            // Restore the original response body stream
    //            response.Body = originalBodyStream;
    //        }
    //    }
    //}
    //public class CompressionHelper
    //{
    //    public static byte[] DeflateByte(byte[] str)
    //    {
    //        if (str == null)
    //        {
    //            return null;
    //        }

    //        using var output = new MemoryStream();
    //        using (var compressor = new GZipStream(output, CompressionMode.Compress))
    //        {
    //            //var compressor2 = new GZipStream(output, System.IO.Compression.CompressionLevel.Fastest);
    //            compressor.Write(str, 0, str.Length);
    //        }

    //        return output.ToArray();
    //    }
    //}
}