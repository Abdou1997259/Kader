namespace Kader_System.Domain.Dtos.Response.Perm;

public class PermGetRolesPermissionsResponse
{
    public string RoleId { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public List<CheckBox> ListOfCheckBoxes { get; set; } = new();
}
