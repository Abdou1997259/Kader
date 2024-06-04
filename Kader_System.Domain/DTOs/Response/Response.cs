namespace Kader_System.Domain.Dtos.Response;

public class Response<T> where T : class
{
    public string Msg { get; set; } = string.Empty;
    public bool Check { get; set; }
    public bool IsActive { get; set; }
    public T Data { get; set; } = default!;
    public string Error { get; set; } = string.Empty;
    public object LookUps { get; set; } = null;
}

