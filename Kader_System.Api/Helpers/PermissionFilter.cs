using Kader_System.DataAccesss.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Kader_System.Api.Helpers
{
    public class PermissionFilter(KaderDbContext db) : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            PermissionAttribute permssionAttribute = context.ActionDescriptor.EndpointMetadata.FirstOrDefault(p => p is PermissionAttribute) as PermissionAttribute;
            if (permssionAttribute != null) {
                var user = context.HttpContext.User;
                var claimidentity = user.Identity as ClaimsIdentity;
                if (claimidentity == null || !claimidentity.IsAuthenticated) {

                    context.Result = new ForbidResult();
                }
               
               
                var userId = user.GetUserId();
                var currentTitle =(await db.Users.FirstOrDefaultAsync(u=>u.Id==userId)).CurrentTitleId;

                var isAdmin = await db.UserRoles.FirstOrDefaultAsync(s => s.UserId == userId && s.RoleId == SuperAdmin.RoleId);
                if (isAdmin != null) {

                    goto Checked;


                }



                var permssionOnScreen = (await db.UserPermissions.FirstOrDefaultAsync(p => p.UserId == userId && p.SubScreenId == permssionAttribute.SubScreenId &&p.TitleId== currentTitle))?.Permission;
                if (permssionOnScreen == null)
                {
                    context.Result = new ForbidResult();
                }

                List<int> listOfPermssionAsIntergers = permssionOnScreen.Splitter();
                bool isPermitted = false;
                foreach (var perm in listOfPermssionAsIntergers)
                {
                    if(perm.CastToPerssmison() == permssionAttribute.Permission)
                    {
                        isPermitted = true;
                    }
                }
                if (!isPermitted) { 
                  context.Result=new ForbidResult();
                }



            }

        Checked:;
        }
    }
}
