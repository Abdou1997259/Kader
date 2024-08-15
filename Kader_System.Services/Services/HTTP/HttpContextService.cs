using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Services.Services.HTTP
{
    public class HttpContextService(IHttpContextAccessor httpContextAccessor, IRequestService requestService) : IHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IRequestService _requestService = requestService;

        public string GetRelativeServerPath(string moduleName,string fileName)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null || fileName == null)
                return null;

            var serverPath = GetPhysicalServerPath();
            var cilentId = _requestService.client_id;
            var clinetIndex = serverPath.IndexOf(cilentId);
            var relativeImagePath = Path.Combine(serverPath.Substring(clinetIndex),moduleName, fileName);
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            var imageUrl = Path.Combine(baseUrl, relativeImagePath).Replace("\\", "/");
            return imageUrl;

        }

        public string GetPhysicalServerPath()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context != null && context.Items.ContainsKey("ServerPath"))
            {
                return context.Items["ServerPath"] as string;
            }

            return null;
        }
    }

}
