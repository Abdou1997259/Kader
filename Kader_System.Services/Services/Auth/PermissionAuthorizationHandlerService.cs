namespace Kader_System.Services.Services.Auth;

public class PermissionAuthorizationHandlerService(IHttpContextAccessor accessor) : AuthorizationHandler<PermissionRequirementService>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirementService requirement)
    {
        if (context.User == null)
            return;

        if (accessor!.HttpContext!.Request.Path == Shared.Notify)
        {
            context.Succeed(requirement);
            return;
        }


        bool canAccess;

        var listOfCurrentUserClaims = context.User.Claims;

        if (listOfCurrentUserClaims.Any(c => c.Type == RequestClaims.UserPermission))
        {
            canAccess = await Task.Run(() => listOfCurrentUserClaims.Any(c => c.Type == RequestClaims.UserPermission && c.Value == requirement.Permission && c.Issuer == Shared.KaderSystem));
            if (canAccess)
            {
                context.Succeed(requirement);
                return;
            }
        }


        bool ifTrue = listOfCurrentUserClaims.Where(c => c.Type == RequestClaims.RolePermission).Any(x =>
        !listOfCurrentUserClaims.Where(c => c.Type == RequestClaims.UserPermission)
        .Select(y => y.Value.Split('.').First())
        .Contains(x.Value.Split('.').First()));

        if (ifTrue) 
        {
            canAccess = await Task.Run(() => context.User.Claims.Any(c => c.Type == RequestClaims.RolePermission && c.Value == requirement.Permission && c.Issuer == Shared.KaderSystem));
            if (canAccess)
            {
                context.Succeed(requirement);
                return;
            }
        }


        #region Ancient

        //bool checkHubAuth = _accessor!.HttpContext!.Request.Path == Shared.Notify; /*&& !string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Headers.Authorization);*/
        //if (checkHubAuth)
        //{
        //    context.Succeed(requirement);
        //    return;
        //}

        //canAccess = await Task.Run(() => context.User.Claims.Any(c => c.Type == RolesClaims.RolePermission && c.Value == requirement.Permission && c.Issuer == Shared.KaderSystem));
        //if (canAccess)
        //{
        //    context.Succeed(requirement);
        //    return;
        //}

        #endregion
    }
}
