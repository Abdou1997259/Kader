using Kader_System.Domain.DTOs.Response.Auth;

namespace Kader_System.Domain.Dtos.Response;

public class Response<T> where T : class
{
    public string Msg { get; set; } = string.Empty;
    public bool Check { get; set; }
    public bool IsActive { get; set; }

    public T Data { get; set; } = default!;
    public List<T> DataList { get; set; } = default!;
    public dynamic DynamicData { get; set; } = default!;
    public string Error { get; set; } = string.Empty;
    public object LookUps { get; set; } = null;
    public object LookUpsScreen { get; set; } = null;
}
public class ResponseWithUser<T> : Response<T> where T : class
{
    public List<TitleLookups> titles { get; set; }
    public int CurrentTitleId { get; set; }
    public string UserName { get; set; }
}
public class ResponseForPermession<T> : Response<T> where T : class
{
    public string? title_name_en { get; set; }
    public string? title_name_ar { get; set; }
}


