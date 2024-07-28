using Kader_System.Domain.DTOs.Request.Auth;
using Kader_System.Domain.DTOs.Response;

namespace Kader_System.Services.IServices.Auth;

public interface IAuthService
{
    Task<Response<AuthLoginUserResponse>> LoginUserAsync(AuthLoginUserRequest model);
    Task<Response<string>> LogOutUserAsync();
    Task<Response<string>> AssignPermissionForUser(Guid id, IEnumerable<AssignPermissionRequest> model);
   Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserRequest model, string root, string clientName, string moduleName, UsereEnum userEnum = UsereEnum.None);
    Task<Response<AuthUpdateUserRequest>> UpdateUserAsync(string id, AuthUpdateUserRequest model);
    Task<Response<string>> ShowPasswordToSpecificUserAsync(string id);
    Task<Response<AuthChangePassOfUserResponse>> ChangePasswordAsync(AuthChangePassOfUserRequest model);
    Task<Response<AuthSetNewPasswordRequest>> SetNewPasswordToSpecificUserAsync(AuthSetNewPasswordRequest model);
    Task<Response<string>> SetNewPasswordToSuperAdminAsync(string newPassword);
}
