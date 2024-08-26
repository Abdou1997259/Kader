namespace Kader_System.Domain.Dtos.Response;

public class GetFileNameAndExtension
{
    public int? fileId {  get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
}

