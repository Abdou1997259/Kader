namespace Kader_System.Services.Services.Auth;

public class DbInitSeedsService(RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork) : IDbInitSeedsService
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    #region Claims

    public async Task SeedClaimsForSuperAdmin()
    {
        var superAdminRole = await _roleManager.FindByNameAsync(RolesEnums.Superadmin.ToString());

        List<string> modules = [];

        foreach (var item in Enum.GetValues(typeof(PermissionsModulesEnums)))
            modules.Add(item.ToString()!);

        var allClaims = await _roleManager.GetClaimsAsync(superAdminRole!);
        List<GetPermissionsWithActions> allRoleClaims = [];
        foreach (string module in modules)
            allRoleClaims.AddRange(Permissions.GeneratePermissionsList(module));

        foreach (var roleClaim in allRoleClaims)
            if (!allClaims.Any(c => c.Type == RequestClaims.RolePermission &&  c.Value == roleClaim.ClaimValue))
            {
                //await _roleManager.AddClaimAsync(superAdminRole!, new Claim(RolesClaims.Permission, permission));

                await _unitOfWork.RoleClaims.AddAsync(new ApplicationRoleClaim
                {
                    RoleId = superAdminRole!.Id,
                    ClaimType = RequestClaims.RolePermission,
                    ClaimValue = roleClaim.ClaimValue
                });
                await _unitOfWork.UserClaims.AddAsync(new ApplicationUserClaim()
                {
                    UserId = SuperAdmin.Id,
                    ClaimType = RequestClaims.RolePermission,
                    ClaimValue = roleClaim.ClaimValue
                });

                await _unitOfWork.CompleteAsync();
            }
    }

    #endregion
}
