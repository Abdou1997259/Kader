using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.Auth;
using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.DTOs.Response.Auth;
using Kader_System.Services.IServices.AppServices;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Kader_System.Services.Services.Auth;

public class AuthService(IUnitOfWork unitOfWork, IPermessionStructureService premissionsevice, IMapper mapper, UserManager<ApplicationUser> userManager,
                   JwtSettings jwt, IStringLocalizer<SharedResource> sharLocalizer, ILogger<AuthService> logger,
                   IHttpContextAccessor accessor, SignInManager<ApplicationUser> signInManager,
                   IFileServer fileServer,
                   RoleManager<ApplicationRole> roleManager,
                   KaderDbContext db,
                   IMainScreenService mainScreenService,
                   IHttpContextAccessor httpContextAccessor,
                   IUserContextService userContext
                ) : IAuthService

{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly JwtSettings _jwt = jwt;
    private readonly ILogger<AuthService> _logger = logger;
    private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
    private readonly IHttpContextAccessor _accessor = accessor;
    private readonly IFileServer _fileServer = fileServer;
    private readonly IPermessionStructureService _permissionservice = premissionsevice;
    private readonly IMainScreenService _mainScreenService = mainScreenService;
    private readonly KaderDbContext _db = db;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IUserContextService _userContext = userContext;

    #region Authentication

    public async Task<Response<AuthLoginUserResponse>> LoginUserAsync(AuthLoginUserRequest model)
    {
        string err = _sharLocalizer[Localization.Error];
        var normalizedUserName = _userManager.NormalizeName(model.UserName);
        var test = _db.Database.GetConnectionString();
        var usernormalize = await _userManager.Users.SingleOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);
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
        var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);


        if (!passwordValid)
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
            Token = "Bearer" + " " + new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            ExpiresOn = jwtSecurityToken.ValidTo,


        };
        return new Response<AuthLoginUserResponse>
        {
            IsActive = true,
            Check = true,
            Data = result,
            Error = string.Empty,
            Msg = string.Empty,
        };
    }


    public async Task<Response<UpdateUserRequest>> UpdateUserAsync(string id, string lang,
        UpdateUserRequest model, string moduleName, HrDirectoryTypes userenum = HrDirectoryTypes.User)

    {
        if (id == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.User], id);

            return new Response<UpdateUserRequest>()
            {
                Data = model,
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        var companyList = await _unitOfWork.Companies.GetAllAsync();
        var validCompanyIds = companyList.Select(c => c.Id).ToHashSet();

        if (!model.company_id.All(id => validCompanyIds.Contains(id.Value)))
        {
            var msg = _sharLocalizer[Localization.CurrentIsNotExitedInTitle];
            return new Response<UpdateUserRequest>
            {
                Check = false,
                Msg = msg,
                Data = null
            };
        }

        model.current_title ??= model.title_id.FirstOrDefault();
        model.current_company ??= model.company_id.FirstOrDefault();

        string err = _sharLocalizer[Localization.Error];
        var obj = await _userManager.FindByIdAsync(id);


        var moduleNameWithType = userenum.GetModuleNameWithType(moduleName);
        if (model.image is not null)
        {
            if (obj?.ImagePath != null)
                _fileServer.RemoveFile(moduleName, obj.ImagePath);
            obj.ImagePath = await _fileServer.UploadFileAsync(moduleNameWithType, model.image);
        }
        else
        {
            if (obj?.ImagePath != null)
                _fileServer.RemoveFile(moduleName, obj.ImagePath);
            obj.ImagePath = null;
        }
        obj.UpdateDate = new DateTime().NowEg();
        obj.UpdateBy = _accessor!.HttpContext == null ? string.Empty : _accessor!.HttpContext!.User.GetUserId();

        obj.PhoneNumber = model.phone;
        obj.UserName = model.user_name;
        foreach (var title in model.title_id)
        {
            // Manage user permissions
            var existingUserPermissions = await _unitOfWork.UserPermssionRepositroy
                .GetSpecificSelectTrackingAsync(x => x.TitleId == title && x.UserId == obj.Id, x => x);

            if (existingUserPermissions.Any())
            {
                _unitOfWork.UserPermssionRepositroy.RemoveRange(existingUserPermissions);
                await _unitOfWork.CompleteAsync();
            }

            var titlePermissions = await _unitOfWork.TitlePermissionRepository
                .GetSpecificSelectTrackingAsync(
                    x => x.TitleId == title,
                    select: x => new UserPermission
                    {
                        TitleId = title.Value,
                        UserId = obj.Id,
                        SubScreenId = x.SubScreenId,
                        Permission = x.Permissions
                    }
                );
            await _unitOfWork.UserPermssionRepositroy.AddRangeAsync(titlePermissions);
            await _unitOfWork.CompleteAsync();

        }

        if (model.password != null)
        {
            obj.VisiblePassword = model.password;
        }
        obj.FullName = model.full_name;
        obj.Email = model.email;
        obj.FinancialYear = model.financial_year;

        obj.CompanyYearId = model.financial_year;

        obj.JobId = model.job_title;
        obj.IsActive = model.is_active;
        obj.CompanyId = model.company_id.NulalbleConcater();
        obj.TitleId = model.title_id.NulalbleConcater();
        obj.CurrentTitleId = model.current_title.Value;
        obj.CurrentCompanyId = model.current_company.Value;
        _unitOfWork.Users.Update(obj);
        await _unitOfWork.CompleteAsync();

        return new Response<UpdateUserRequest>()
        {
            Check = true,
            Data = model,
            Msg = _sharLocalizer[Localization.Updated]

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
        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        string err = _sharLocalizer[Localization.Error];

        using var transaction = _unitOfWork.BeginTransaction();

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.User], userId);

            return new Response<AuthSetNewPasswordRequest>()
            {
                Data = model,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        if (user.VisiblePassword != model.OldPassword)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.OldPasswordDoesnotMatch],
                          _sharLocalizer[Localization.User], userId);
            return new Response<AuthSetNewPasswordRequest>()
            {
                Data = model,
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        if (model.NewPassword != model.ConfirmPassword)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.NewPasswordDoesnotMatch],
                                     _sharLocalizer[Localization.User], userId);
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

        var user = await _userManager.FindByIdAsync(SuperAdmins.Ids[0]);

        if (user is null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.User], SuperAdmins.Ids[0]);

            return new Response<string>()
            {
                Data = newPassword,
                Error = resultMsg,
                Msg = resultMsg
            };
        }


        var result = await _userManager.ChangePasswordAsync(user, SuperAdmins.Password, newPassword);


        //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        user.VisiblePassword = SuperAdmins.Password = newPassword;

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
            new Claim(RequestClaims.Company,user.CompanyId.ToString()),
            new Claim(RequestClaims.UserId, user.Id),
            new Claim(RequestClaims.Mobile,user.PhoneNumber),
            new Claim(RequestClaims.FullName,user.FullName),
            new Claim(RequestClaims.Titles,user.TitleId),
            new Claim(RequestClaims.Email,user.Email),
            new Claim(RequestClaims.Image,user.ImagePath ?? " "),
            new Claim(RequestClaims.CurrentCompany,user.CurrentCompanyId.ToString()),
            new Claim(RequestClaims.CurrentTitle,user.CurrentTitleId.ToString())
        }
        .Union(roleClaims)
        .Union(listOfUserClaims);
        //.Union(await _userManager.GetClaimsAsync(user));

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        return new JwtSecurityToken(
            issuer: _jwt.Issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(2).Add(_jwt.TokenLifetime),
            signingCredentials: signingCredentials);
    }

    //public async Task<Response<CreateUserResponse>> UpdateUserAsync(Guid id, 
    //     CreateUserRequest request, string root, string clientName, 

    //    string moduleName, UsereEnum userenum = UsereEnum.None)
    //{
    //    var user = await _userManager.FindByIdAsync(id.ToString());
    //    var msg = _sharLocalizer[Localization.NotFoundData];
    //    if (user == null)
    //    {

    //        return new()
    //        {
    //            Check = false,
    //            Data = null,
    //            Msg = msg
    //        };
    //    }



    //    // Map CreateUserRequest to ApplicationUser
    //    var mappeduser=_mapper.Map(request,user);

    //    // Set additional user properties if needed
    //    mappeduser.Id = Guid.NewGuid().ToString();
    //    var full_path = Path.Combine(root, clientName, moduleName);
    //    var moduleNameWithType = userenum.GetModuleNameWithType(moduleName);
    //    if (request.image != null)
    //        _fileServer.RemoveFile(full_path, mappeduser.ImagePath);
    //    mappeduser.ImagePath = (request.image == null || request.image.Length == 0) ? null :
    //       await _fileServer.UploadFileAsync(root, clientName, moduleNameWithType, request.image);

    //    _unitOfWork.Users.Update(mappeduser);
    //    await _unitOfWork.CompleteAsync();

    //     msg = _sharLocalizer[Localization.Updated];
    //    return new()
    //    {
    //        Check = true,
    //        Data = null,
    //        Msg = msg
    //    };

    //}
    public async Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserRequest model,
        string moduleName, HrDirectoryTypes hrDirectoryTypes = HrDirectoryTypes.User)
    {
        if (await _unitOfWork.Users.ExistAsync(x => x.UserName == model.user_name))
        {
            var msg = _sharLocalizer[Localization.IsExist];
            return new Response<CreateUserResponse>
            {
                Check = false,
                Msg = msg,
                Data = null
            };
        }

        var companyList = await _unitOfWork.Companies.GetAllAsync();
        var validCompanyIds = companyList.Select(c => c.Id).ToHashSet();

        if (!model.company_id.All(id => validCompanyIds.Contains(id.Value)))
        {
            var msg = _sharLocalizer[Localization.CurrentIsNotExitedInTitle];
            return new Response<CreateUserResponse>
            {
                Check = false,
                Msg = msg,
                Data = null
            };
        }

        model.current_title ??= model.title_id.FirstOrDefault();
        model.current_company ??= model.company_id.FirstOrDefault();

        if (!model.company_id.Contains(model.current_company))
        {
            var msg = _sharLocalizer[Localization.CurrentIsNotExitedInCompanys];
            return new Response<CreateUserResponse>
            {
                Check = false,
                Msg = msg,
                Data = null
            };
        }

        if (!model.title_id.Contains(model.current_title))
        {
            var msg = _sharLocalizer[Localization.CurrentIsNotExitedInCompanys];
            return new Response<CreateUserResponse>
            {
                Check = false,
                Msg = msg,
                Data = null
            };
        }

        using (var transaction = new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                var user = _mapper.Map<ApplicationUser>(model);
                user.VisiblePassword = model.password;
                user.PhoneNumber = model.phone;
                user.FullName = model.full_name;
                user.Email = model.email;
                user.FinancialYear = model.financial_year;
                user.CompanyId = model.company_id.NulalbleConcater();
                user.TitleId = model.title_id.NulalbleConcater();
                user.CurrentTitleId = model.current_title.Value;
                user.CurrentCompanyId = model.current_company.Value;
                user.JobId = model.job_title;
                user.UserName = model.user_name;
                user.Add_date = DateTime.UtcNow;
                user.CompanyYearId = model.financial_year;
                user.Added_by = _accessor.HttpContext?.User.GetUserId() ?? string.Empty;
                user.Id = Guid.NewGuid().ToString();

                var moduleNameWithType = hrDirectoryTypes.GetModuleNameWithType(moduleName);
                if (model.image != null && model.image.Length > 0)
                {
                    user.ImagePath = await _fileServer.UploadFileAsync(moduleNameWithType, model.image);
                }

                var result = await _userManager.CreateAsync(user, model.password);
                if (!result.Succeeded)
                {
                    var errorMsg = _sharLocalizer[Localization.Error];
                    return new Response<CreateUserResponse>
                    {
                        Check = false,
                        Msg = string.Join(',', result.Errors.Select(e => e.Description)),
                        Data = null
                    };
                }

                var token = await CreateJwtToken(user); // Implement this method if needed

                var response = new CreateUserResponse
                {
                    CompanyId = model.company_id,
                    FullName = model.full_name,
                    Email = model.email,
                    JobTitle = model.job_title,
                    CompanyYear = DateTime.Now.Year,
                    TitleId = model.title_id,
                    Token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token),
                    UserName = model.user_name,
                };

                await AddPermissionByTitleToUser(model.title_id, user.Id);
                transaction.Complete(); // Commit the transaction scope

                return new Response<CreateUserResponse>
                {
                    Check = true,
                    Data = response,
                    DynamicData = user.Id
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    Msg = string.Format(_sharLocalizer[Localization.Error],
                        _sharLocalizer[Localization.Employee]),
                    Check = false,
                    Data = null,
                    Error = ex.InnerException != null ? ex.InnerException.ToString() : ex.Message
                };
            }
        }
    }

    private async Task AddPermissionByTitleToUser(List<int?> titles, string userId)
    {
        foreach (var title in titles)
        {
            // Manage user permissions
            var existingUserPermissions = await _unitOfWork.UserPermssionRepositroy
                .GetSpecificSelectTrackingAsync(x => x.TitleId == title && x.UserId == userId, x => x);

            if (existingUserPermissions.Any())
            {
                _unitOfWork.UserPermssionRepositroy.RemoveRange(existingUserPermissions);
                await _unitOfWork.CompleteAsync();
            }

            var titlePermissions = await _unitOfWork.TitlePermissionRepository
                .GetSpecificSelectTrackingAsync(
                    x => x.TitleId == title,
                    select: x => new UserPermission
                    {
                        TitleId = x.TitleId
                        ,
                        UserId = userId,
                        SubScreenId = x.SubScreenId,
                        Permission = x.Permissions
                    }
                );
            await _unitOfWork.UserPermssionRepositroy.AddRangeAsync(titlePermissions);
            await _unitOfWork.CompleteAsync();

        }
    }


    public async Task<Response<string>> AssignPermissionForUser(string id, bool all, int? titleId, IEnumerable<Permissions> model, string lang)
    {
        var user = (await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Id == id));
        if (user is null)
        {
            var msg = _sharLocalizer[Localization.CannotBeFound, _sharLocalizer[Localization.User]];
            return new Response<string>
            {
                Msg = msg,
                Check = false,
                Data = "",
            };
        }
        if (titleId == 0 || titleId == null)
        {
            var msg = _sharLocalizer[Localization.TitlePermisson];
            return new Response<string>
            {
                Msg = msg,
                Check = false,
                Data = "",
            };
        }
        var subMainScreenActions = await _unitOfWork.SubMainScreenActions.GetAllAsync();
        foreach (var sub in model)
        {
            // Get ActionIds for the current SubId
            var actionIdsForSubId = subMainScreenActions
                .Where(x => x.ScreenSubId == sub.SubId)
                .Select(x => x.ActionId)
                .Distinct()
                .ToList();

            // Get TitlePermssion for the current SubId
            var titlePermissions = sub.title_permission;

            // Check if there is at least one ActionId that is not in the TitlePermssion
            var missingActionsExist = titlePermissions.Except(actionIdsForSubId);

            // Process the result
            if (missingActionsExist.Any() && missingActionsExist.Any(x => x != 0))
            {
                var permssions = await _unitOfWork.ActionsRepo.GetSpecificSelectAsync(x => missingActionsExist.Any(u => u == x.Id), x => x);
                var subscrren = await _unitOfWork.SubScreens.GetByIdAsync(sub.SubId);
                string name = Localization.Arabic == lang ? subscrren.Screen_sub_title_ar : subscrren.Screen_sub_title_en;
                string nameofpermissions = "";
                foreach (var per in permssions)
                {
                    nameofpermissions += Localization.Arabic == lang ? per.Name + " " : per.NameInEnglish + " ";
                }
                var msg = $"{name} {_sharLocalizer[Localization.ScreenInAction]} {nameofpermissions}";
                // Handle the case where at least one ActionId is missing
                // Example: Log or perform some action
                return new Response<string>()
                {
                    Check = false,
                    Msg = msg,
                    Data = null

                };

            }
        }

        var userPermissions = model.Select(x => new UserPermission
        {
            UserId = id,
            Permission = string.Join(',', x.title_permission),
            SubScreenId = x.SubId,
            TitleId = titleId ?? 0
        }).ToList();

        // Process Title Permissions
        foreach (var assignedPermission in model)
        {

            if (await _unitOfWork.SubScreens.ExistAsync(x => x.Id == assignedPermission.SubId))
            {

                var titlePermission = await _unitOfWork.TitlePermissionRepository
                .GetSpecificSelectAsync(x =>
                x.TitleId == titleId && x.SubScreenId == assignedPermission.SubId, x => x);


                if (titlePermission.Count() > 0)
                {
                    unitOfWork.TitlePermissionRepository.RemoveRange(titlePermission);
                }
                if (assignedPermission.title_permission.Count == 0 || assignedPermission.title_permission.Any(x => x == 0))
                {
                    continue;
                }
                else
                {
                    await _unitOfWork.TitlePermissionRepository.AddAsync(new TitlePermission
                    {
                        TitleId = titleId ?? 0,
                        SubScreenId = assignedPermission.SubId,
                        Permissions = assignedPermission.title_permission.Concater()
                    });
                }



            }
            else
            {
                var msg = _sharLocalizer[Localization.InvalidSubId];
                return new Response<string>()
                {
                    Check = false,
                    Data = null,
                    Msg = msg


                };
            }

        }

        await _unitOfWork.CompleteAsync();

        // Process User Permissions
        var result = await ProcessUserPermissions(id, titleId ?? 0, model, all);
        if (!result.Check)
        {
            return new Response<string>
            {
                Check = result.Check,
                Data = null,
                Msg = result.Msg
            };
        }

        return new Response<string>
        {
            Check = true,
            Data = "",
        };
    }

    private async Task<Response<string>> ProcessUserPermissions(string userId, int titleId, IEnumerable<Permissions> model, bool all)
    {
        foreach (var assignedPermission in model)
        {
            var userPermissionQuery = await _unitOfWork.UserPermssionRepositroy
                .GetSpecificSelectTrackingAsync(x => x.TitleId == titleId && x.SubScreenId == assignedPermission.SubId, x => x);

            if (await _unitOfWork.SubScreens.ExistAsync(x => x.Id == assignedPermission.SubId))
            {
                if (userPermissionQuery.Count() > 0)
                {
                    _unitOfWork.UserPermssionRepositroy.RemoveRange(userPermissionQuery);
                    await _unitOfWork.CompleteAsync();

                }

                if (assignedPermission.title_permission.Count == 0 || assignedPermission.title_permission.Any(x => x == 0))
                {
                    continue;
                }
                else
                {
                    var newPermission = new UserPermission
                    {
                        UserId = userId.ToString(),
                        TitleId = titleId,
                        SubScreenId = assignedPermission.SubId,
                        Permission = string.Join(',', assignedPermission.title_permission)
                    };

                    // Detach any existing entity with the same Id


                    await _unitOfWork.UserPermssionRepositroy.AddAsync(newPermission);
                }
            }
            else
            {
                var msg = _sharLocalizer[Localization.InvalidSubId];
                return new Response<string>()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };
            }
        }

        await _unitOfWork.CompleteAsync();
        return new Response<string>()
        {
            Check = true,
            Data = null,
        };

    }



    public async Task<Response<GetAllUsersResponse>> GetAllUsers(FilterationUsersRequest model, string host, string lang, string moduleName, HrDirectoryTypes userenum = HrDirectoryTypes.User)
    {
        Expression<Func<ApplicationUser, bool>> filter = x => x.IsDeleted == model.IsDeleted &&
            (string.IsNullOrEmpty(model.Word) || x.Email.Contains(model.Word) ||
           (string.IsNullOrEmpty(model.Word) || x.UserName.Contains(model.Word) ||
           (string.IsNullOrEmpty(model.Word) || x.FullName.Contains(model.Word)


           ))

            );
        var companise = await _unitOfWork.Companies.GetAllAsync();
        var jobs = await _unitOfWork.Jobs.GetAllAsync();
        var totalRecords = await _unitOfWork.Users.CountAsync(filter: filter);
        int page = 1;
        int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
        if (model.PageNumber < 1)
            page = 1;
        else
            page = model.PageNumber;
        var pageLinks = Enumerable.Range(1, totalPages)
            .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
            .ToList();

        // Fetch the necessary data from the database
        var users = await _unitOfWork.Users.GetSpecificSelectAsync(
          filter: filter,
            take: model.PageSize,
            skip: (model.PageNumber - 1) * model.PageSize,

            select: x => new
            {
                x.Id,
                x.CompanyId,
                x.CurrentTitleId,
                x.CurrentCompanyId,
                x.FinancialYear,
                x.Email,
                x.JobId,
                x.PhoneNumber,
                x.UserName,
                x.FullName,

                x.ImagePath

            }

        );


        var folderPath = userenum.GetModuleNameWithType(moduleName);

        // Perform the in-memory transformations
        var items = users.Select(x => new ListOfUsersResponse
        {
            Id = x.Id,
            CurrentTitle = x.CurrentTitleId,
            CompanyName = Localization.Arabic == lang
                ? companise.FirstOrDefault(c => c.Id == x.CurrentCompanyId)?.NameAr
                : companise.FirstOrDefault(c => c.Id == x.CurrentCompanyId)?.NameEn,
            FinancialYear = x.FinancialYear,
            Email = x.Email,
            JobName = Localization.Arabic == lang
                ? jobs.FirstOrDefault(j => j.Id == x.JobId)?.NameAr
                : jobs.FirstOrDefault(j => j.Id == x.JobId)?.NameEn,
            Phone = x.PhoneNumber,

            UserName = x.UserName,
            FullName = x.FullName,
            Image = Path.Combine(folderPath, x.ImagePath ?? "")


        }).ToList();

        var result = new GetAllUsersResponse
        {
            TotalRecords = totalRecords,

            Items = items,
            CurrentPage = model.PageNumber,
            FirstPageUrl = host + $"?PageSize={model.PageSize}&PageNumber=1&IsDeleted={model.IsDeleted}",
            From = (page - 1) * model.PageSize + 1,
            To = Math.Min(page * model.PageSize, totalRecords),
            LastPage = totalPages,
            LastPageUrl = host + $"?PageSize={model.PageSize}&PageNumber={totalPages}&IsDeleted={model.IsDeleted}",
            PreviousPage = page > 1 ? host + $"?PageSize={model.PageSize}&PageNumber={page - 1}&IsDeleted={model.IsDeleted}" : null,
            NextPageUrl = page < totalPages ? host + $"?PageSize={model.PageSize}&PageNumber={page + 1}&IsDeleted={model.IsDeleted}" : null,
            Path = host,
            PerPage = model.PageSize,
            Links = pageLinks
        };

        if (result.TotalRecords is 0)
        {
            string resultMsg = _sharLocalizer[Localization.NotFoundData];

            return new()
            {
                Data = new()
                {
                    Items = []
                },
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        return new()
        {
            Data = result,
            Check = true
        };
    }

    public async Task<Response<IEnumerable<ListOfUsersResponse>>> ListListOfUsers(string lang)
    {
        var companise = await _unitOfWork.Companies.GetAllAsync();
        var jobs = await _unitOfWork.Jobs.GetAllAsync();
        var users = await _unitOfWork.Users.GetSpecificSelectAsync(null!,
            select: x => new
            {
                x.Id,
                x.CompanyId,
                x.FinancialYear,
                x.Email,
                x.JobId,
                x.PhoneNumber,
                x.UserName,
                x.FullName

            },
            orderBy: x => x.OrderByDescending(x => x.Id)
        );

        // Perform the in-memory transformations
        var result = users.Select(x => new ListOfUsersResponse
        {

            //CompanyName = Localization.Arabic == lang
            //    ? companise.FirstOrDefault(c => c.Id == x.CurrentCompanyId)?.NameAr
            //    : companise.FirstOrDefault(c => c.Id == x.CurrentCompanyId)?.NameEn,
            FinancialYear = x.FinancialYear,
            Email = x.Email,
            JobName = Localization.Arabic == lang
                ? jobs.FirstOrDefault(j => j.Id == x.JobId)?.NameAr
                : jobs.FirstOrDefault(j => j.Id == x.JobId)?.NameEn,
            Phone = x.PhoneNumber,
            Id = x.Id,
            UserName = x.FullName
        }).ToList();

        if (!result.Any())
        {
            string resultMsg = _sharLocalizer[Localization.NotFoundData];

            return new()
            {
                Data = [],
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        return new()
        {
            Data = result,
            Check = true
        };
    }


    public async Task<Response<GetUserByIdResponse>> GetUserById(string id, string lang, string moduleName, HrDirectoryTypes hrDirectory)

    {


        var obj = await _userManager.FindByIdAsync(id.ToString());
        var companise = await _unitOfWork.Companies.GetByIdAsync(obj.CurrentCompanyId);
        var jobs = await _unitOfWork.Jobs.GetByIdAsync(obj.JobId);
        if (obj is null)
        {
            string resultMsg = _sharLocalizer[Localization.NotFoundData];

            return new()
            {
                Data = new(),
                Error = resultMsg,
                Msg = resultMsg
            };
        }


        var lookups = await UsersGetLookups(lang);
        var trimmedTitleId = obj.TitleId.Trim(',').Trim();

        // Split the string by comma, trim each part, and parse to int
        var titleIdList = trimmedTitleId
            .Split(',')
            .Select(x => x.Trim())
            .Where(x => int.TryParse(x, out _)) // Optional: Ensure only valid integers are included
            .Select(int.Parse)
            .ToList();


        var pathOfModule = hrDirectory.GetModuleNameWithType(moduleName);
        var theFullPath = _fileServer.CombinePath(pathOfModule, obj.ImagePath);
        return new()
        {
            Data = new()
            {
                Id = obj.Id,
                FullName = obj.FullName,
                Phone = obj?.PhoneNumber ?? " ",
                CompanyId = obj.CurrentCompanyId,
                Companys = obj.CompanyId.Splitter(),
                CurrentTitle = obj.CurrentTitleId,
                FinancialYear = obj.CompanyYearId,
                Email = obj.Email,
                JobTitle = obj.JobId,
                TitleId = titleIdList,
                IsActive = obj.IsActive,
                Image = theFullPath,
                Password = null,
                UserName = obj.UserName


            },
            LookUps = lookups,
            Check = true
        };
    }

    public async Task<Response<UsersLookups>> UsersGetLookups(string lang)
    {
        var jobs = (await _unitOfWork.Jobs.GetAllAsync()).Select(x => new JobsLookups
        {
            Id = x.Id,
            JobName = Localization.Arabic == lang ? x.NameAr : x.NameEn
        });

        var compaines = (await _unitOfWork.Companies.GetAllAsync()).Select(x => new CompanyLookup
        {
            Id = x.Id,
            CompnayName = Localization.Arabic == lang ? x.NameAr : x.NameEn
        });

        var titles = (await _unitOfWork.Titles.GetAllAsync()).Select(x => new TitleLookups
        {
            Id = x.Id,
            TitleName = Localization.Arabic == lang ? x.TitleNameAr : x.TitleNameEn
        });

        var FinancalYear = await _unitOfWork.Users.GetCompanyYearsAsync();




        return new Response<UsersLookups>()
        {
            Check = true,
            Data = new()
            {
                FinancalYear = FinancalYear,
                Companies = compaines,
                Titles = titles,
                Jobs = jobs

            }
        };
    }

    public async Task<Response<string>> DeleteUser(string id)
    {
        var obj = await _userManager.FindByIdAsync(id.ToString());

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.User]);

            return new()
            {
                Data = string.Empty,
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        obj.IsDeleted = true;

        obj.DeleteDate = new DateTime().NowEg();
        obj.DeleteBy = _accessor!.HttpContext == null ? string.Empty : _accessor!.HttpContext!.User.GetUserId();

        _unitOfWork.Users.Update(obj);
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Check = true,
            Data = string.Empty,
            Msg = _sharLocalizer[Localization.Deleted]
        };
    }

    public async Task<Response<string>> RestoreUser(string id)
    {
        var obj = await _userManager.FindByIdAsync(id.ToString());
        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.User]);

            return new()
            {
                Data = null,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        obj.IsDeleted = false;
        obj.UpdateDate = new DateTime().NowEg();
        obj.UpdateBy = _accessor!.HttpContext == null ? string.Empty : _accessor!.HttpContext!.User.GetUserId();
        _unitOfWork.Users.Update(obj);
        await _unitOfWork.CompleteAsync();
        return new()
        {
            Check = true,
            Data = _sharLocalizer[Localization.Restored],
            Msg = _sharLocalizer[Localization.Restored]
        };
    }

    public async Task<Response<GetMyProfileResponse>> GetMyProfile(string lang, string moduleName, HrDirectoryTypes hrDirectory)
    {
        var userId = _userContext.UserId;

        var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Id == userId);

        var companies = await _unitOfWork.Companies.GetSpecificSelectAsQuerableAsync(x => user.CompanyId.Splitter().Contains(x.Id), x => x);
        var titles = await _unitOfWork.Titles.GetSpecificSelectAsync(x => user.TitleId.Splitter().Contains(x.Id), x => x);

        Kader_System.Domain.Models.Title title = null;
        List<int> inttitiles = user.TitleId.Splitter();

        var containedTitles = new HashSet<string>((await _unitOfWork.UserPermssionRepositroy.GetSpecificSelectAsync(x => inttitiles.Contains(x.TitleId) && x.UserId == userId, select: x => x.TitleId.ToString())));
        var allTitles = await _unitOfWork.Titles.GetSpecificSelectAsQuerableAsync(x => containedTitles.Contains(x.Id.ToString()), x => new { Id = x.Id, TitleNameAr = x.TitleNameAr, TitleNameEn = x.TitleNameEn });

        HrCompany cop = null;

        title = await _unitOfWork.Titles.GetByIdAsync(user.CurrentTitleId);



        cop = await _unitOfWork.Companies.GetByIdAsync(user.CurrentCompanyId);

        //var screens = await _mainScreenService.GetMainScreensWithRelatedDataAsync(lang);
        var jwtSecurityToken = await CreateJwtToken(await _userManager.FindByIdAsync(user.Id));

        var email = user.Email;
        var fullName = user.FullName;
        var title2 = Localization.Arabic == lang ? title?.TitleNameAr ?? string.Empty : title?.TitleNameEn ?? string.Empty;
        var mobile = user.PhoneNumber;


        var pathFolder = hrDirectory.GetModuleNameWithType(moduleName);
        var fullPath = Path.Combine(pathFolder, user.ImagePath ?? "");
        var image = fullPath;
        var currentTitles = user.CurrentTitleId;
        var currentCompany = user.CurrentCompanyId;
        var currentCompanyName = Localization.Arabic == lang ? cop?.NameAr ?? string.Empty : cop?.NameEn ?? string.Empty;




        //var screensResult = screens?.DataList;
        var aptoken = "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        // Fetch companies asynchronously
        var companiesList = (companies).Select(x => new Companys
        {

            id = x.Id,
            name = Localization.Arabic == lang ? x.NameAr : x.NameEn
        }
        ).ToList();

        var permissionScreenData = await _unitOfWork.StoredProcuduresRepo.SpGetScreen(userId, currentTitles, lang);
        var obj = new GetMyProfileResponse
        {
            ApiToken = aptoken,
            Email = email,
            FullName = fullName,
            Title = title2,
            Mobile = mobile,
            Image = image,
            Titles = allTitles.Select(x => new TitleLookups
            {
                Id = x.Id,
                TitleName = Localization.Arabic == lang ? x.TitleNameAr : x.TitleNameEn
            }),
            user = new Domain.DTOs.Response.Auth.User
            {
                CurrentTitles = currentTitles,
                CurrentCompany = currentCompany,
                Companys = companiesList,
                CurrentYear = 2033,
                Years = 2023,
                CurrentCompanyName = currentCompanyName,
                Mypermissions = permissionScreenData.myPermissions,
                Screens = permissionScreenData.getAllStMainScreens
            }
        };

        return new Response<GetMyProfileResponse>
        {
            Check = true,
            Data = obj,
        };
    }

    public async Task<Response<string>> ChangeTitle(int title)
    {
        var userId = (_accessor!.HttpContext!.User as ClaimsPrincipal).GetUserId();
        var user = await _userManager.FindByIdAsync(userId);
        if (!user.TitleId.Splitter().Contains(title))
        {
            var msg = _sharLocalizer[Localization.UserInTitle];
            return new()
            {
                Msg = msg,
                Data = null,
                Check = false
            };
        }





        await ChangeSpecificClaim(user.Id, RequestClaims.CurrentTitle, title.ToString());
        user.CurrentTitleId = title;
        _unitOfWork.Users.Update(user);

        await _unitOfWork.CompleteAsync();
        return new()
        {
            Check = true,
            Data = "Updated"
        };


    }
    public async Task<Response<string>> ChangeCompany(int company)
    {
        var userId = (_accessor!.HttpContext!.User as ClaimsPrincipal).GetUserId();
        var user = await _userManager.FindByIdAsync(userId);
        if (!user.CompanyId.Splitter().Contains(company))
        {
            var msg = _sharLocalizer[Localization.UserInCompany];
            return new()
            {
                Msg = msg,
                Data = null,
                Check = false
            };
        }




        user.CurrentCompanyId = company;
        await ChangeSpecificClaim(user.Id, RequestClaims.CurrentCompany, company.ToString());
        _unitOfWork.Users.Update(user);
        await _unitOfWork.CompleteAsync();


        return new()
        {
            Check = true,
            Data = "Updated"
        };


    }

    public async Task<Response<IEnumerable<TitleLookups>>> GetTitleLookUps(string id, string lang)
    {
        var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
        {
            var msg = _sharLocalizer[Localization.NotFound];
            return new()
            {
                Msg = msg,
                Check = false,
                Data = null,
            };
        }
        var titles = await _unitOfWork.Titles.GetSpecificSelectAsync(x => user.TitleId.Splitter().Contains(x.Id), x => x);
        return new()
        {
            Check = true,
            Data = titles.Select(x => new TitleLookups
            {
                Id = x.Id,
                TitleName = Localization.Arabic == lang ? x.TitleNameAr : x.TitleNameEn
            })
        };
    }

    public async Task<Response<string>> ChangeSpecificClaim(string userId, string claimType, string newValue)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return new()
            {
                Check = false,
                Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.User]]
            };


        }
        var existingClaims = await _userManager.GetClaimsAsync(user);

        var claimToRemove = existingClaims.FirstOrDefault(x => x.Type == claimType);

        if (claimToRemove != null)
        {

            await _userManager.RemoveClaimAsync(user, claimToRemove);
        }

        var newClaim = new Claim(RequestClaims.CurrentCompany, newValue);

        await _userManager.AddClaimAsync(user, newClaim);

        return new()
        {
            Check = true,
            Msg = _sharLocalizer[Localization.Updated]
        };
    }

    #endregion
}
