
using Kader_System.Domain.DTOs.Request.Auth;
using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.DTOs.Response.Auth;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.Auth;

public interface IAuthService
{
    Task<Response<AuthLoginUserResponse>> LoginUserAsync(AuthLoginUserRequest model);
    Task<Response<string>> LogOutUserAsync();

    public  Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserRequest model,
       string moduleName, HrDirectoryTypes userenum = HrDirectoryTypes.User);
    Task<Response<string>> ShowPasswordToSpecificUserAsync(string id);
    Task<Response<AuthChangePassOfUserResponse>> ChangePasswordAsync(AuthChangePassOfUserRequest model);
    Task<Response<AuthSetNewPasswordRequest>> SetNewPasswordToSpecificUserAsync(AuthSetNewPasswordRequest model);
    Task<Response<string>> SetNewPasswordToSuperAdminAsync(string newPassword);
    Task<Response<GetAllUsersResponse>> GetAllUsers(FilterationUsersRequest model, string host,string lang, string moduleName, HrDirectoryTypes userenum = HrDirectoryTypes.User);
    Task<Response<IEnumerable<ListOfUsersResponse>>> ListListOfUsers(string lang);
    Task<Response<GetUserByIdResponse>> GetUserById(string id, string lang, string moduleName, HrDirectoryTypes hrDirectory= HrDirectoryTypes.User);
    Task<Response<UsersLookups>> UsersGetLookups(string lang);

    Task<Response<UpdateUserRequest>> UpdateUserAsync(string id, string lang,
         UpdateUserRequest model, string moduleName, HrDirectoryTypes userenum= HrDirectoryTypes.User );

    Task<Response<string>> DeleteUser(string id);
    Task<Response<string>> RestoreUser(string id);
    Task<Response<string>> AssignPermissionForUser(string id, bool all, int? titleId, IEnumerable<Permissions> model,string lang);

    Task<Response<GetMyProfileResponse>> GetMyProfile(string lang, string moduleName, HrDirectoryTypes hrDirectory);
    Task<Response<string>> ChangeTitle(int title);
    Task<Response<string>> ChangeCompany(int company);

    Task<Response<IEnumerable<TitleLookups>>> GetTitleLookUps(string id, string lang);

}
