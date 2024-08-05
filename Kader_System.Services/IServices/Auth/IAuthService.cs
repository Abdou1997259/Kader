
using Kader_System.Domain.DTOs.Request.Auth;
using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.DTOs.Response.Auth;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.Auth;

public interface IAuthService
{
    Task<Response<AuthLoginUserResponse>> LoginUserAsync(AuthLoginUserRequest model);
    Task<Response<string>> LogOutUserAsync();

    Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserRequest model, string root, string clientName, string moduleName, UsereEnum userEnum = UsereEnum.None);
    //Task<Response<AuthUpdateUserRequest>> UpdateUserAsync(string id, AuthUpdateUserRequest model);
    Task<Response<string>> ShowPasswordToSpecificUserAsync(string id);
    Task<Response<AuthChangePassOfUserResponse>> ChangePasswordAsync(AuthChangePassOfUserRequest model);
    Task<Response<AuthSetNewPasswordRequest>> SetNewPasswordToSpecificUserAsync(AuthSetNewPasswordRequest model);
    Task<Response<string>> SetNewPasswordToSuperAdminAsync(string newPassword);
    Task<Response<GetAllUsersResponse>> GetAllUsers(FilterationUsersRequest model, string host, string lang);
    Task<Response<IEnumerable<ListOfUsersResponse>>> ListListOfUsers(string lang);
    Task<Response<GetUserByIdResponse>> GetUserById(string id, string lang);
    Task<Response<UsersLookups>> UsersGetLookups(string lang);

    Task<Response<UpdateUserRequest>> UpdateUserAsync(string id, string lang,
         UpdateUserRequest model, string root, string clientName, string moduleName, UsereEnum userenum = UsereEnum.None);

    Task<Response<string>> DeleteUser(string id);
    Task<Response<string>> RestoreUser(string id);
    Task<Response<string>> AssignPermissionForUser(string id, bool all, int titleId, IEnumerable<Permissions> model,string lang);

    Task<Response<GetMyProfileResponse>> GetMyProfile(string lang);
    Task<Response<string>> ChangeTitle(int title);
    Task<Response<string>> ChangeCompany(int company);

}
