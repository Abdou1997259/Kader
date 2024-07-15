using Kader_System.Services.IServices.HTTP;

public class RequestService : Kader_System.Services.IServices.HTTP.ITitleService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetRequestHeaderLanguage
    {
        get
        {
            var acceptLanguageHeader = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();
            return acceptLanguageHeader.Split(',').FirstOrDefault() ?? "ar";
        }
    }
    public string GetCurrentHost
    {
        get
        {
            var host = _httpContextAccessor.HttpContext.Request.Host.Value +
                       _httpContextAccessor.HttpContext.Request.Path.Value;
            return host;
        }
    }
}
