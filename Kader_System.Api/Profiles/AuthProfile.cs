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
        CreateMap<CreateUserRequest, ApplicationUser>().ForMember(d => d.PhoneNumber, s => s.MapFrom(x => x.Phone));
        CreateMap<CreateUserRequest, ApplicationUser>().ForMember(d => d.TitleId, s => s.MapFrom(x => string.Join(',',x.TitleId)));
        #endregion
    }
}
