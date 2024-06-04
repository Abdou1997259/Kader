using Microsoft.AspNetCore.Identity;

namespace Kader_System.Services.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwt;
    private readonly ILogger<AuthService> _logger;
    private readonly IStringLocalizer<SharedResource> _sharLocalizer;
    private readonly IHttpContextAccessor _accessor;

    public AuthService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager,
                       JwtSettings jwt, IStringLocalizer<SharedResource> sharLocalizer, ILogger<AuthService> logger,
                       IHttpContextAccessor accessor, SignInManager<ApplicationUser> signInManager,
                       RoleManager<ApplicationRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _jwt = jwt;
        _sharLocalizer = sharLocalizer;
        _logger = logger;
        _accessor = accessor;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    #region Authentication

    public async Task<Response<AuthLoginUserResponse>> LoginUserAsync(AuthLoginUserRequest model)
    {
        string err = _sharLocalizer[Localization.Error];
       
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
            return new()
            {
                Data = new()
                {
                    UserName = model.UserName
                },
                Error = err,
                Msg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.UserName]),
                Check = false
            };

        var test = await _signInManager.PasswordSignInAsync(user.UserName!, model.Password, model.RememberMe, false);
        if (!test.Succeeded)
            return new()
            {
                Data = new()
                {
                    UserName = model.UserName
                },
                Error = err,
                Msg = _sharLocalizer[Localization.PasswordNotmatch],
                Check = false
            };

        if (!user.IsActive)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.NotActive],
                _sharLocalizer[Localization.User], model.UserName);

            return new()
            {
                Data = new()
                {
                    UserName = model.UserName
                },
                Error = resultMsg,
                Msg = resultMsg
            };
        }


        //if (model.DeviceId != null)
        //{
        //    //user.DeviceId = model.DeviceId;

        //    await _unitOfWork.UserDevices.AddAsync(new ApplicationUserDevice
        //    {
        //        UserId = user.Id,
        //        DeviceId = model.DeviceId
        //    });

        //    await _unitOfWork.CompleteAsync();
        //}

        var currentUserRoles = (await _userManager.GetRolesAsync(user)).ToList();
        //string superAdminRole = Domain.Constants.Enums.RolesEnums.Superadmin.ToString().Trim();

        var jwtSecurityToken = await CreateJwtToken(user);
        var result = new AuthLoginUserResponse
        {
            UserName = user.UserName!,
            Email = user.Email!,
            RoleNames = currentUserRoles,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            ExpiresOn = jwtSecurityToken.ValidTo,
            
            
        };

        return new Response<AuthLoginUserResponse>
        {
            IsActive = true,
            Check = true,
            Data = result,
            Error = string.Empty,
            Msg = string.Empty
        };
    }

    public async Task<Response<AuthUpdateUserRequest>> UpdateUserAsync(string id, AuthUpdateUserRequest model)
    {
        if (id == null || model.Id != id)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.User], id);

            return new Response<AuthUpdateUserRequest>()
            {
                Data = model,
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        string err = _sharLocalizer[Localization.Error];

        var obj = _mapper.Map<AuthUpdateUserRequest, ApplicationUser>(model, (await _userManager.FindByIdAsync(id))!);
        obj.UpdateDate = new DateTime().NowEg();
        obj.UpdateBy = _accessor!.HttpContext == null ? string.Empty : _accessor!.HttpContext!.User.GetUserId();

        bool isSucceeded = (await _userManager.UpdateAsync(obj)).Succeeded;

        return new Response<AuthUpdateUserRequest>()
        {
            Check = isSucceeded,
            Data = model,
            Msg = isSucceeded ? _sharLocalizer[Localization.Updated] : _sharLocalizer[err]
        };
    }

    public async Task<Response<string>> ShowPasswordToSpecificUserAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user is null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.User], id);

            return new Response<string>()
            {
                Data = id,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        string err = _sharLocalizer[Localization.Error];

        return new Response<string>()
        {
            Check = true,
            Data = user.VisiblePassword
        };
    }

    public async Task<Response<AuthChangePassOfUserResponse>> ChangePasswordAsync(AuthChangePassOfUserRequest model)
    {
        string err = _sharLocalizer[Localization.Error];

        var mappedResponse = _mapper.Map<AuthChangePassOfUserResponse>(model);

        if (model.CurrentPassword == model.NewPassword)
        {
            string resultMsg = _sharLocalizer[Localization.CurrentAndNewPasswordIsTheSame];

            return new Response<AuthChangePassOfUserResponse>()
            {
                Data = mappedResponse,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        string userId = _accessor!.HttpContext == null ? string.Empty : _accessor!.HttpContext!.User.GetUserId();
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.User], userId);

            return new Response<AuthChangePassOfUserResponse>()
            {
                Data = mappedResponse,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (!result.Succeeded)
        {
            string resultMsg = _sharLocalizer[Localization.CurrentPasswordIsIncorrect];

            return new Response<AuthChangePassOfUserResponse>()
            {
                Data = mappedResponse,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        if (!user.IsActive)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.NotActive],
                _sharLocalizer[Localization.User], user.UserName);

            return new Response<AuthChangePassOfUserResponse>()
            {
                Data = mappedResponse,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        return new Response<AuthChangePassOfUserResponse>()
        {
            Msg = string.Format(_sharLocalizer[Localization.Updated]),
            Check = true,
            Data = mappedResponse
        };
    }

    public async Task<Response<AuthSetNewPasswordRequest>> SetNewPasswordToSpecificUserAsync(AuthSetNewPasswordRequest model)
    {
        string err = _sharLocalizer[Localization.Error];

        using var transaction = _unitOfWork.BeginTransaction();

        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user is null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.User], model.UserId);

            return new Response<AuthSetNewPasswordRequest>()
            {
                Data = model,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

        user.VisiblePassword = model.NewPassword;
        _unitOfWork.Users.Update(user);
        await _unitOfWork.CompleteAsync();

        if (!result.Succeeded)
        {
            string resultMsg = _sharLocalizer[Localization.CurrentPasswordIsIncorrect];

            return new Response<AuthSetNewPasswordRequest>()
            {
                Data = model,
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        transaction.Commit();
        return new Response<AuthSetNewPasswordRequest>()
        {
            Msg = string.Format(_sharLocalizer[Localization.Updated]),
            Check = true,
            Data = model
        };
    }

    public async Task<Response<string>> SetNewPasswordToSuperAdminAsync(string newPassword)
    {
        string err = _sharLocalizer[Localization.Error];

        using var transaction = _unitOfWork.BeginTransaction();

        var user = await _userManager.FindByIdAsync(SuperAdmin.Id);

        if (user is null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.User], SuperAdmin.Id);

            return new Response<string>()
            {
                Data = newPassword,
                Error = resultMsg,
                Msg = resultMsg
            };
        }


        var result = await _userManager.ChangePasswordAsync(user, SuperAdmin.Password, newPassword);


        //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        user.VisiblePassword = SuperAdmin.Password = newPassword;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.CompleteAsync();

        if (!result.Succeeded)
        {
            string resultMsg = _sharLocalizer[Localization.CurrentPasswordIsIncorrect];

            return new Response<string>()
            {
                Data = newPassword,
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        transaction.Commit();
        return new Response<string>()
        {
            Msg = string.Format(_sharLocalizer[Localization.Updated]),
            Check = true,
            Data = newPassword
        };

    }

    public async Task<Response<string>> LogOutUserAsync()
    {
        string userId = GetUserId();
        var lsitOfObjects = await _unitOfWork.UserDevices.GetAllAsync(x => x.UserId == userId);

        if (!lsitOfObjects.Any())
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.User], userId);

            return new Response<string>()
            {
                Data = string.Empty,
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        string err = _sharLocalizer[Localization.Error];
        try
        {
            _unitOfWork.UserDevices.RemoveRange(lsitOfObjects);

            bool result = await _unitOfWork.CompleteAsync() > 0;

            if (!result)
                return new Response<string>()
                {
                    Check = false,
                    Data = userId,
                    Error = err,
                    Msg = err
                };
            return new Response<string>()
            {
                Check = true,
                Data = userId,
                Msg = _sharLocalizer[Localization.Deleted]
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, err);

            return new Response<string>()
            {
                Error = err,
                Msg = ex.Message + (ex.InnerException == null ? string.Empty : ex.InnerException.Message)
            };
        }
    }

    private string GetUserId() =>
        _accessor!.HttpContext == null ? string.Empty : _accessor!.HttpContext!.User.GetUserId();
    private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
    {
        List<Claim> roleClaims = [];
        List<Claim> listOfUserClaims = [];

        var roles = (await _userManager.GetRolesAsync(user)).ToList();

        foreach (var role in roles)
        {
            var dd = await _roleManager.FindByNameAsync(role);

            var ddd = _roleManager.GetClaimsAsync(dd!).Result;
            roleClaims.AddRange(ddd);
        }
        
        var userClaims = _userManager.GetClaimsAsync(user!).Result;
        listOfUserClaims.AddRange(userClaims);

        for (int i = 0; i < roles.Count; i++)
            roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(RequestClaims.UserId, user.Id)
        }
        .Union(roleClaims)
        .Union(listOfUserClaims);
        //.Union(await _userManager.GetClaimsAsync(user));

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        return new JwtSecurityToken(
            issuer: _jwt.Issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2).Add(_jwt.TokenLifetime),
            signingCredentials: signingCredentials);
    }

    #endregion
}
