using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.Auth;
using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.DTOs.Response.Auth;
using Kader_System.Domain.Models;
using Kader_System.Domain.Models.EmployeeRequests;
using Kader_System.Domain.Models.Setting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using static Kader_System.Domain.Constants.SD.ApiRoutes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kader_System.Services.Services.Auth;

public class AuthService(IUnitOfWork unitOfWork, IUserPermessionService premissionsevice, IMapper mapper, UserManager<ApplicationUser> userManager,
                   JwtSettings jwt, IStringLocalizer<SharedResource> sharLocalizer, ILogger<AuthService> logger,
                   IHttpContextAccessor accessor, SignInManager<ApplicationUser> signInManager,
                   IFileServer fileServer,
                   RoleManager<ApplicationRole> roleManager


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
    private readonly IUserPermessionService _permissionservice = premissionsevice;


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
            Token = "Bearer" + " " +  new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
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

    public async Task<Response<UpdateUserRequest>> UpdateUserAsync(string id,string lang, 
        UpdateUserRequest model, string root, string clientName, string moduleName, UsereEnum userenum = UsereEnum.None)
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
        string err = _sharLocalizer[Localization.Error];
        var obj = await _userManager.FindByIdAsync(id);
       

        var full_path = Path.Combine(root, clientName, moduleName);
        var moduleNameWithType = userenum.GetModuleNameWithType(moduleName);
        if (model.image != null)
            _fileServer.RemoveFile(full_path, obj.ImagePath);

       
        obj.ImagePath = (model.image == null || model.image.Length == 0) ? " " :
           await _fileServer.UploadFile(root, clientName, moduleNameWithType, model.image);
        obj.UpdateDate = new DateTime().NowEg();
        obj.UpdateBy = _accessor!.HttpContext == null ? string.Empty : _accessor!.HttpContext!.User.GetUserId();
       
        obj.PhoneNumber = model.phone;
        obj.UserName = model.user_name;

        if (model.password != null)
        {
            obj.VisiblePassword= model.password;
        }
        obj.FullName = model.full_name;
        obj.Email = model.email;
        obj.FinancialYear = model.financial_year;
        obj.CompanyId = model.company_id.Concater();

       obj.TitleId=model.title_id.Concater();
        obj.JobId = model.job_title;
        obj.IsActive = model.is_active;
        obj.CurrentTitleId = model.current_title;
        obj.CurrentCompanyId = model.current_company;
        _unitOfWork.Users.Update(obj);
      await  _unitOfWork.CompleteAsync();

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
    //       await _fileServer.UploadFile(root, clientName, moduleNameWithType, request.image);

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
    public async  Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserRequest model,
        string root, string clientName, string moduleName, UsereEnum userenum= UsereEnum.None)
    {  // Check if user already exists
        var isExited = await _unitOfWork.Users.ExistAsync(x => x.UserName == model.user_name);
        if (isExited)
        {
            var msg = _sharLocalizer[Localization.IsExist];
            return new Response<CreateUserResponse>
            {
                Check = false,
                Msg = msg,
                Data = null
            };
        }
        
        // Map CreateUserRequest to ApplicationUser
        var user = _mapper.Map<ApplicationUser>(model);

        user.VisiblePassword = model.password;
        user.PhoneNumber = model.phone;
        user.FullName = model.full_name;
        user.Email = model.email;
        user.FinancialYear= model.financial_year;
        user.CompanyId = model.company_id.Concater();
        user.JobId = model.job_title;
        user.UserName = model.user_name;
        user.Add_date = new DateTime().NowEg();
        user.CurrentCompanyId = model.current_company;
        user.CurrentTitleId = model.current_title;
        user.Added_by = _accessor!.HttpContext == null ? string.Empty : _accessor!.HttpContext!.User.GetUserId();
        // Set additional user properties if needed
        user.Id = Guid.NewGuid().ToString();

      
        var moduleNameWithType = userenum.GetModuleNameWithType(moduleName);
        user.ImagePath = (model.image == null || model.image.Length == 0) ? null :
            await _fileServer.UploadFile(root, clientName, moduleNameWithType, model.image);
      

       
       var titlepermissions = await _unitOfWork.TitlePermissionRepository.GetByIdAsync(model.current_title);
       var userpermission = new UserPermission
        {

                UserId = user.Id,
                TitleId = titlepermissions.TitleId,
                SubScreenId = titlepermissions.SubScreenId,
                Permission = titlepermissions.Permissions
       };
       await  _unitOfWork.UserPermssionRepositroy.AddAsync(userpermission);

        
     
      

        // Add user to the database
        await _unitOfWork.Users.AddAsync(user);
     

     
        var result = await _unitOfWork.CompleteAsync();
        if (result <= 0)
        {
            var errorMsg = _sharLocalizer[Localization.Error];
            return new Response<CreateUserResponse>
            {
                Check = false,
                Msg = errorMsg,
                Data = null
            };
        }

        // Optionally, generate JWT token
        var token = await CreateJwtToken(user); // Implement this method if needed
        var resutl = new CreateUserResponse
        {
            CompanyId = model.company_id,
            FullName = model.full_name,
            Email = model.email,
            JobTitle = model.job_title,
            CompanyYear = 2023,
            TitleId=model.title_id,
            Token = "Bearer" + " " + new JwtSecurityTokenHandler().WriteToken(token),
            UserName = model.user_name,
        };
        // Return success response
        return new Response<CreateUserResponse>
        {
            Check = true,
            Data =resutl ,
          // Include the token if you generated it
        };



    }

    public async Task<Response<string>> AssignPermissionForUser(string id, bool all, int titleId, IEnumerable<Permissions> model)
    {
        if (titleId == 0)
        {
            return new Response<string>
            {
                Check = false,
                Data = "",
            };
        }

        var userPermissions = model.Select(x => new UserPermission
        {
            UserId = id,
            Permission = string.Join(',', x.TitlePermssion),
            SubScreenId = x.SubId,
            TitleId = titleId
        }).ToList();

        // Process Title Permissions
        foreach (var assignedPermission in model)
        {
            var titlePermission = await _unitOfWork.TitlePermissionRepository
                .GetSpecificSelectAsync(x =>
                x.TitleId == titleId && x.SubScreenId == assignedPermission.SubId, x => x);

            if (titlePermission.Any())
            {
                var listUpdatedPer = titlePermission.Select(x => new TitlePermission
                {
                    Id=x.Id,
                    Permissions = string.Join(',', assignedPermission.SubId),
                    SubScreenId = x.SubScreenId,
                    TitleId = x.TitleId
                });
                _unitOfWork.TitlePermissionRepository.UpdateRange(listUpdatedPer);
            }
            else
            {
                // Check if the SubScreenId exists in the st_screens_subs table
                var subScreenExists = await _unitOfWork.SubMainScreens.AnyAsync(x => x.Id == assignedPermission.SubId);

                if (subScreenExists)
                {
                    await _unitOfWork.TitlePermissionRepository.AddAsync(new TitlePermission
                    {
                        TitleId = titleId,
                        SubScreenId = assignedPermission.SubId,
                        Permissions = string.Join(',', assignedPermission.TitlePermssion)
                    });
                }
                else
                {
                    // Log the invalid SubScreenId or handle accordingly
                    Console.WriteLine("Invalid SubScreenId: " + assignedPermission.SubId);
                    continue;
                }
            }
        }

        await _unitOfWork.CompleteAsync();

        // Process User Permissions
        await ProcessUserPermissions(id, titleId, model, all);

        return new Response<string>
        {
            Check = true,
            Data = "",
        };
    }

    private async Task ProcessUserPermissions(string userId, int titleId, IEnumerable<Permissions> model, bool all)
    {
        int userCounter = 0;
        foreach (var assignedPermission in model)
        {
            var userPermissionQuery = await _unitOfWork.UserPermssionRepositroy
                .GetSpecificSelectAsync(x => x.TitleId == titleId && x.SubScreenId == assignedPermission.SubId, x => x);

            var userPermission = all ? userPermissionQuery : userPermissionQuery.Where(x => x.UserId == userId);

            if (userPermission.Any())
            {
                var listUpdatedPer = userPermission.Select(x => new UserPermission
                {
                    Id=x.Id,
                    UserId=x.UserId,
                    Permission = string.Join(',', assignedPermission.TitlePermssion),
                    SubScreenId = x.SubScreenId,
                    TitleId = x.TitleId
                });
                _unitOfWork.UserPermssionRepositroy.UpdateRange(listUpdatedPer);
            }
            else
            {
                // Check if the SubScreenId exists in the st_screens_subs table
                var subScreenExists = await _unitOfWork.SubMainScreens.AnyAsync(x => x.Id == assignedPermission.SubId);

                if (subScreenExists)
                {
                    await _unitOfWork.UserPermssionRepositroy.AddAsync(new UserPermission
                    { 

                        UserId=userId.ToString(),
                       
                        TitleId = titleId,
                        SubScreenId = assignedPermission.SubId,
                        Permission = string.Join(',', assignedPermission.TitlePermssion)
                    });
                }
                else
                {
                    // Log the invalid SubScreenId or handle accordingly
                    Console.WriteLine("Invalid SubScreenId: " + assignedPermission.SubId);
                    continue;
                }
            }
        }

        await _unitOfWork.CompleteAsync();
    }


    public async Task<Response<GetAllUsersResponse>> GetAllUsers(FilterationUsersRequest model,string host,string lang)
    {
        Expression<Func<ApplicationUser, bool>> filter = x => x.IsDeleted == model.IsDeleted &&
            (string.IsNullOrEmpty(model.Word) || x.Email.Contains(model.Word) 
             
            
            );
        var companise =await _unitOfWork.Companies.GetAllAsync();
        var jobs=await _unitOfWork.Jobs.GetAllAsync();
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
                x.UserName
            },
            orderBy: x => x.OrderByDescending(x => x.Add_date)
        );

        // Perform the in-memory transformations
        var items = users.Select(x => new ListOfUsersResponse
        {
            Id=x.Id,
            CompanyName = Localization.Arabic == lang
                ? companise.FirstOrDefault(c => c.Id == x.CurrentCompanyId)?.NameAr
                : companise.FirstOrDefault(c => c.Id == x.CurrentCompanyId)?.NameEn,
            FinancialYear = x.FinancialYear,
            Email = x.Email,
            JobName = Localization.Arabic == lang
                ? jobs.FirstOrDefault(j => j.Id == x.JobId)?.NameAr
                : jobs.FirstOrDefault(j => j.Id == x.JobId)?.NameEn,
            Phone = x.PhoneNumber,
            UserName=x.UserName
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
            Id=x.Id,
            UserName=x.UserName
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

    public async Task<Response<GetUserByIdResponse>> GetUserById(string id,string lang)
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
        return new()
        {
            Data = new()
            {
                Id = obj.Id,
                FullName = obj.FullName,
                Phone = obj?.PhoneNumber ?? " ",
                CompanyId = obj.CurrentCompanyId,
                Companys = obj.CompanyId.Splitter(),
                CurrentTitle=obj.CurrentTitleId,
                FinancialYear = obj.FinancialYear,
                Email = obj.Email,
                JobTitle = obj.JobId,
                TitleId = titleIdList,
                IsActive = obj.IsActive,
                Image = obj.ImagePath,
                Password = null,
                UserName = obj.UserName

            },
            LookUps = lookups,
            Check = true
        };
    }

    public async Task<Response<UsersLookups>> UsersGetLookups(string lang)
    {
        var jobs =( await _unitOfWork.Jobs.GetAllAsync()).Select(x=>new JobsLookups
        {
            Id=x.Id,
            JobName=Localization.Arabic==lang? x.NameAr: x.NameEn
        });

        var compaines = (await _unitOfWork.Companies.GetAllAsync()).Select(x=>new CompanyLookup
        {
            Id = x.Id,  
            CompnayName=Localization.Arabic ==lang? x.NameAr: x.NameEn  
        });

        var titles =( await _unitOfWork.Titles.GetAllAsync()).Select(x=>new TitleLookups
        {
            Id=x.Id,
            TitleName=Localization.Arabic==lang?x.TitleNameAr:x.TitleNameEn
        });

        var FinancalYear = new List<FinancalYear>
        {
            new FinancalYear
            {
                Id=1,
                Year=2025
            }
            ,   new FinancalYear
            {
                Id=2,
                Year=2022
            }
            ,   new FinancalYear
            {
                Id=3,
                Year=2021
            }

        };



        return new Response<UsersLookups>()
        {
            Check= true,    
            Data = new()
            {
                FinancalYear=FinancalYear,
                Companies=compaines,
                Titles=titles,
                Jobs=jobs

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

    public async Task<Response<GetMyProfileResponse>> GetMyProfile(string lang)
    {
      var user=   _accessor!.HttpContext!.User as ClaimsPrincipal;

        var companies = await _unitOfWork.Companies.GetSpecificSelectAsync(x => user.GetCompaines().Splitter().Contains(x.Id), x => x);
        var titles = await _unitOfWork.Titles.GetSpecificSelectAsync(x => user.GetTitles().Splitter().Contains(x.Id), x => x);
    
        Kader_System.Domain.Models.Title title = null;
        HrCompany cop = null;
        if (!string.IsNullOrEmpty(user.GetCurrentTitle()))
        {
             title = await _unitOfWork.Titles.GetByIdAsync(int.Parse(user.GetCurrentTitle()));
        }
        if (!string.IsNullOrEmpty(user.GetCurrentCompany()))
        {
            cop = await _unitOfWork.Companies.GetByIdAsync(int.Parse(user.GetCurrentCompany()));
        }

        var screens = await _mainScreenService.GetMainScreensWithRelatedDataAsync(lang);
        var perm = await _permissionservice.GetAllUserPermession(user.GetUserId(),lang);
        var jwtSecurityToken =await  CreateJwtToken(await _userManager.FindByIdAsync(user.GetUserId()));   
      
       var obj = new GetMyProfileResponse()
       {

           ApiToken = "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
           Email = user?.GetEmalil() ?? string.Empty,
           FullName = user?.GetFullName() ?? string.Empty,
           Title = Localization.Arabic == lang ? title?.TitleNameAr ?? string.Empty : title?.TitleNameEn ?? string.Empty,
           Mobile = user?.GetMobile() ?? string.Empty,
           Image = user?.GetImage() ?? string.Empty,
           user = new Domain.DTOs.Response.Auth.User
           {
               CurrentTitles = int.Parse(user.GetCurrentTitle()),
               CurrentCompany = int.Parse(user.GetCurrentCompany()),
               Companys = await companies.AsQueryable().ToDynamicLookUpAsync("Id", "CompnayName"),
               CurrentYear=2033,
               Years=2023,
               CurrentCompanyName= Localization.Arabic == lang ? cop.NameAr : cop.NameEn,
               Mypermissions=perm.DataList,
               Screens= screens.Data


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
        var userId =(_accessor!.HttpContext!.User as ClaimsPrincipal).GetUserId();
        var user =await _userManager.FindByIdAsync(userId);
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
        var oldpermissions = await _unitOfWork.UserPermssionRepositroy.GetSpecificSelectAsync(x => x.UserId == userId, x => x);
      
        _unitOfWork.UserPermssionRepositroy.RemoveRange(oldpermissions);
        var titlepermissions = await _unitOfWork.TitlePermissionRepository.GetByIdAsync(title);
        var userpermission = new UserPermission
        {

            UserId = user.Id,
            TitleId = titlepermissions.TitleId,
            SubScreenId = titlepermissions.SubScreenId,
            Permission = titlepermissions.Permissions
        };
        await _unitOfWork.UserPermssionRepositroy.AddAsync(userpermission);

        return new()
        {
            Check = true,
            Data = "Updated"
        };

       
    }

    #endregion
}
