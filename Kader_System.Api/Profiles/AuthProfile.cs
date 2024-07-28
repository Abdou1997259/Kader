using Kader_System.Domain.DTOs.Request.Auth;

namespace Kader_System.Api.Profiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        #region Auth

        CreateMap<ApplicationUser, AuthRegisterUserRequest>()
                .ReverseMap();

        CreateMap<ApplicationUser, AuthRegisterUserResponse>()
                .ReverseMap();

        CreateMap<ApplicationUser, AuthRegisterUserResponse>()
                .ReverseMap();

        CreateMap<ApplicationUser, AuthGetAllUsersResponse>()
                .ReverseMap();

        CreateMap<AuthRegisterUserResponse, AuthRegisterUserRequest>()
                .ReverseMap();

        CreateMap<AuthUpdateUserResponse, AuthUpdateUserRequest>()
                .ReverseMap();

        CreateMap<ApplicationUser, AuthUpdateUserRequest>()
                .ReverseMap();

        CreateMap<AuthChangePassOfUserResponse, AuthChangePassOfUserRequest>()
                .ReverseMap();

        CreateMap<CreateUserRequest, ApplicationUser>().ForMember(d => d.VisiblePassword, s => s.MapFrom(x => x.Password));

        #endregion
    }
}
