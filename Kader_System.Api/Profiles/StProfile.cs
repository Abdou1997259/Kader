using Kader_System.Domain.DTOs.Request.Setting;

namespace Kader_System.Api.Profiles;

public class StProfile : Profile
{
    public StProfile()
    {
        #region Setting

        CreateMap<StScreenSub, StUpdateSubMainScreenRequest>()
                .ReverseMap();

        CreateMap<StResonsiblity, CreateStResonsiblityRequest>().ReverseMap();

        CreateMap<StResonsiblity, UpdateStResonsiblityRequest>().ReverseMap();




        #endregion
    }
}
