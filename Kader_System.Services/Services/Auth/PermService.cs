namespace Kader_System.Services.Services.Auth;

public class PermService(UserManager<ApplicationUser> userManager, IStringLocalizer<SharedResource> sharLocalizer,
                         RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork, IHttpContextAccessor accessor) : IPermService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _accessor = accessor;

    #region Roles

    public async Task<Response<IEnumerable<SelectListForUserResponse>>> GetAllRolesAsync(string lang)
    {
        var result =
                await _unitOfWork.Roles.GetSpecificSelectAsync(null!,
                select: x => new SelectListForUserResponse
                {
                    Id = x.Id,
                    Name = Localization.Arabic == lang ? x.Title_name_ar : x.Name!
                }, orderBy: x =>
                  x.OrderByDescending(x => x.Id));

        if (!result.Any())
        {
            string resultMsg = _sharLocalizer[Localization.NotFoundData];

            return new()
            {
                Data = new List<SelectListForUserResponse>(),
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        return new()
        {
            //Msg = _sharLocalizer[Localization.Displayed],
            Data = result,
            Check = true
        };
    }

    public async Task<Response<PermCreateRoleRequest>> CreateRoleAsync(PermCreateRoleRequest model)
    {
        string err = _sharLocalizer[Localization.Error];

        if (await _roleManager.RoleExistsAsync(model.Name.ToLower()))
            return new Response<PermCreateRoleRequest>()
            {
                Data = model,
                Error = err,
                Msg = string.Format(_sharLocalizer[Localization.IsExist],
                    _sharLocalizer[Localization.Role]),
                Check = false
            };
        var role = new ApplicationRole()
        {
            Name = model.Name
        };
        role.Added_by = role.UpdateBy = GetUserId();
        role.Add_date = role.UpdateDate = new DateTime().NowEg();
        role.Title_name_ar = model.Title_name_ar;

        var result = await _roleManager.CreateAsync(role);
        if (result.Succeeded)
            return new()
            {
                Msg = _sharLocalizer[Localization.Done],
                Check = true,
                Data = model,
            };
        return new()
        {
            Error = result.Errors.Select(x => x.Description).First(),
            Msg = err
        };
    }

    public async Task<Response<PermUpdateRoleRequest>> UpdateRoleAsync(string id, PermUpdateRoleRequest model)
    {
        var role = await _roleManager.FindByIdAsync(id);

        if (role is null || id != model.Id)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Role], $"({id})");



            return new Response<PermUpdateRoleRequest>()
            {
                Data = new(),
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        string err = _sharLocalizer[Localization.Error];

        role.Name = model.Name;
        role.UpdateBy = GetUserId();
        role.UpdateDate = new DateTime().NowEg();
        role.Title_name_ar = model.Title_name_ar;

        var idResult = await _roleManager.UpdateAsync(role);

        return new Response<PermUpdateRoleRequest>()
        {
            Check = idResult.Succeeded,
            Data = model,
            Msg = idResult.Succeeded ? _sharLocalizer[Localization.Updated] : _sharLocalizer[err]
        };
    }

    public async Task<Response<string>> DeleteRoleByIdAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        if (role == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Role], $"({id})");

            return new Response<string>()
            {
                Data = string.Empty,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        var allUsersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);

        if (allUsersInRole.Any())
        {
            string resultMsg = _sharLocalizer[Localization.CannotDeletedThisRole];

            return new Response<string>()
            {
                Data = string.Empty,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        string err = _sharLocalizer[Localization.Error];

        var roleClaims = _roleManager.GetClaimsAsync(role).Result;

        if (roleClaims.Any())
        {
            if (roleClaims.Count > 1)
                roleClaims.ToList().ForEach(async claim => await _roleManager.RemoveClaimAsync(role, claim));
            else
                await _roleManager.RemoveClaimAsync(role, roleClaims.First());
        }

        var obj = await _roleManager.DeleteAsync(role);

        bool result = obj.Succeeded;

        if (!result)
            return new Response<string>()
            {
                Check = false,
                Data = id,
                Error = err,
                Msg = err
            };
        return new Response<string>()
        {
            Check = true,
            Data = id,
            Msg = _sharLocalizer[Localization.Deleted]
        };
    }

    #endregion

    #region Spicial to roles of user

    public async Task<Response<PermGetAllUsersRolesResponse>> GetEachUserWithHisRolesAsync(PermGetEachUserWithRolesRequest model)
    {
        Expression<Func<ApplicationUser, bool>> filter = x => x.IsActive && !x.IsDeleted
        &&
        (model.CreationStartDate == null || x.Add_date!.Value.Date >= model.CreationStartDate.Value.Date)
        &&
        (model.CreationEndDate == null || x.Add_date!.Value.Date <= model.CreationEndDate.Value.Date);

        var result = new PermGetAllUsersRolesResponse
        {
            TotalRecords = await _unitOfWork.Users.CountAsync(filter),
            Data = [.. _userManager.Users.Where(filter)
            .ToList()
            .Skip((model.PageNumber - 1) * model.PageSize)
            .Take(model.PageSize)
            .Select(user => new PermGetAllUsersRoles
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                Roles = [.. _userManager.GetRolesAsync(user).Result]
            })
            ]
        };

        return new()
        {
            Data = result,
            Check = true
        };
    }

    public async Task<Response<PermGetManagementModelResponse>> ManageUserRolesAsync(string userId)
    {

        var user = await _userManager.FindByIdAsync(userId);
        var roles = _roleManager.Roles.Where(x => x.Id != "fab4fac1-c546-41de-aebc-a14da68957ab1").ToList();

        var result = new PermGetManagementModelResponse
        {
            UserId = user!.Id,
            UserName = user.UserName!,
            ListOfCheckBoxes = roles.Select(role => new CheckBox
            {
                DisplayValue = role.Name!,
                IsSelected = _userManager.IsInRoleAsync(user, role.Name!).Result
            }).ToList()
        };

        if (result is null)
        {
            string resultMsg = _sharLocalizer[Localization.NotFoundData];

            return new Response<PermGetManagementModelResponse>()
            {
                Data = new PermGetManagementModelResponse(),
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        return new Response<PermGetManagementModelResponse>()
        {
            Data = result,
            Check = true
        };
    }

    public async Task<Response<PermGetManagementModelResponse>> UpdateUserRolesAsync(PermGetManagementModelResponse model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        var userRoles = await _userManager.GetRolesAsync(user!);

        await _userManager.RemoveFromRolesAsync(user!, userRoles);
        await _userManager.AddToRolesAsync(user!, model.ListOfCheckBoxes.Where(x => x.IsSelected).Select(y => y.DisplayValue));

        return new Response<PermGetManagementModelResponse>()
        {
            Data = model,
            Msg = _sharLocalizer[Localization.Updated],
            Check = true
        };

    }

    #endregion

    #region Spicial to permissions(Claims) of role

    public async Task<Response<PermGetPermissionsToSpecificRoleResponse>> ManageRolePermissionsAsync(string roleId, string lang)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

        if (role == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Role], roleId);
            return new()
            {
                Data = null!,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        var roleClaims = _roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();

        var eachsubMainWithActions = await _unitOfWork.SubMainScreenActions.GetEachSubMainWithActionsAsync(lang);

        foreach (var action in eachsubMainWithActions.SelectMany(x => x.Actions))
            if (roleClaims.Any(c => c == action.ClaimValue))
                action.IsSelected = true;

        var result = new PermGetPermissionsToSpecificRoleResponse()
        {
            RoleId = roleId,
            RoleName = role.Name!,
            EachSubMainWithActions = [.. eachsubMainWithActions]
        };

        return new()
        {
            Data = result,
            Check = true
        };
    }
    public async Task<Response<PermUpdateRolePermissionsRequest>> UpdateRolePermissionsAsync(PermUpdateRolePermissionsRequest model)
    {

        var role = await _roleManager.FindByIdAsync(model.RoleId);

        if (role == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Role], model.RoleId);

            return new()
            {
                Data = null!,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        var roleClaims = await _roleManager.GetClaimsAsync(role);//true

        var falseValues = model.ActionsWithClaimValues.Where(y => !y.IsSelected).Select(x => x.ClaimValue).ToList();

        var removedClaims = roleClaims.Where(x => falseValues.Contains(x.Value)).ToList();

        if (removedClaims.Count != 0)
            foreach (Claim claim in removedClaims)
                await _roleManager.RemoveClaimAsync(role, claim);


        var trueValues = model.ActionsWithClaimValues.Where(c => c.IsSelected && !roleClaims.Select(v => v.Value).Contains(c.ClaimValue)).ToList();

        if (trueValues.Count != 0)
            foreach (var claim in trueValues)
                await _roleManager.AddClaimAsync(role, new Claim(RequestClaims.RolePermission, claim.ClaimValue));

        return new()
        {
            Data = model,
            Check = true,
            Msg = _sharLocalizer[Localization.Updated]
        };
    }

    #endregion

    #region Spicial to permissions(Claims) of user

    public async Task<Response<PermGetPermissionsToSpecificUserResponse>> ManageUserPermissionsAsync(string userId, string lang)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.User], userId);
            return new()
            {
                Data = null!,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        var userClaims = _userManager.GetClaimsAsync(user).Result.Select(c => c.Value).ToList();
        //var userClaim1s = _userManager.GetUsersForClaimAsync();

        var eachsubMainWithActions = await _unitOfWork.SubMainScreenActions.GetEachSubMainWithActionsAsync(lang);

        foreach (var action in eachsubMainWithActions.SelectMany(x => x.Actions))
            if (userClaims.Any(c => c == action.ClaimValue))
                action.IsSelected = true;

        var result = new PermGetPermissionsToSpecificUserResponse()
        {
            UserId = userId,
            UserName = user.UserName!,
            EachSubMainWithActions = [.. eachsubMainWithActions]
        };

        return new()
        {
            Data = result,
            Check = true
        };
    }
    public async Task<Response<PermUpdateUserPermissionsRequest>> UpdateUserPermissionsAsync(PermUpdateUserPermissionsRequest model)
    {

        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.User], model.UserId);

            return new()
            {
                Data = null!,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        var userClaims = await _userManager.GetClaimsAsync(user);//true

        var falseValues = model.ActionsWithClaimValues.Where(y => !y.IsSelected).Select(x => x.ClaimValue).ToList();

        var removedClaims = userClaims.Where(x => falseValues.Contains(x.Value)).ToList();

        if (removedClaims.Count != 0)
            foreach (Claim claim in removedClaims)
                await _userManager.RemoveClaimAsync(user, claim);


        var trueValues = model.ActionsWithClaimValues.Where(c => c.IsSelected && !userClaims.Select(v => v.Value).Contains(c.ClaimValue)).ToList();

        if (trueValues.Count != 0)
            foreach (var claim in trueValues)
                await _userManager.AddClaimAsync(user, new Claim(RequestClaims.UserPermission, claim.ClaimValue));

        return new()
        {
            Data = model,
            Check = true,
            Msg = _sharLocalizer[Localization.Updated]
        };
    }

    #endregion

    private string GetUserId() =>
        _accessor!.HttpContext == null ? string.Empty : _accessor!.HttpContext!.User.GetUserId();
}
