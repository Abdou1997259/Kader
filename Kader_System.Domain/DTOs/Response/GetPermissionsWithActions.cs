namespace Kader_System.Domain.Dtos.Response;

public class GetPermissionsWithActions
{
    public int ActionId { get; set; }
    public string ClaimValue { get; set; } = string.Empty;
}
