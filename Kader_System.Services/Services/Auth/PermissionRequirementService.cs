namespace Kader_System.Services.Services.Auth;

public class PermissionRequirementService(string permission) : IAuthorizationRequirement
{
    public string Permission { get; private set; } = permission;
}
