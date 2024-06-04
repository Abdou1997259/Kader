namespace Kader_System.Domain.Dtos.Request.Perm;

public class PermUpdateRolePermissionsRequest
{
    public required string RoleId { get; set; }
    public List<ActionsWithClaimValue> ActionsWithClaimValues { get; set; } = [];
}
