using Kader_System.Domain.DTOs.Request.Interview;
using Kader_System.Domain.Models.Interviews;

namespace Kader_System.Api.Profiles
{
    public class Interview : Profile
    {
        public Interview()
        {
            CreateMap<CreateApplicantRequest, Applicant>()
                .ForMember(x => x.Educations, otp => otp.Ignore())
                .ForMember(x => x.Experiences, opt => opt.Ignore())
                .ForMember(x => x.CvFilesPath, opt => opt.Ignore())
                .ForMember(x => x.ImagePath, opt => opt.Ignore())

                ;
        }
    }
}
