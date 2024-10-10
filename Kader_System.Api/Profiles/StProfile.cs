namespace Kader_System.Api.Profiles;

public class StProfile : Profile
{
    public StProfile()
    {
        #region Setting

        CreateMap<StScreenSub, StUpdateSubScreenRequest>()
                .ReverseMap();





        #endregion
    }
}
