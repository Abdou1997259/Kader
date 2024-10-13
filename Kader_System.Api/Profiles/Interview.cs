using Kader_System.Domain.DTOs.Request.Interview;
using Kader_System.Domain.Models.Interviews;

namespace Kader_System.Api.Profiles
{
    public class Interview : Profile
    {
        public Interview()
        {
            CreateMap<CreateApplicantRequest, Applicant>()
                .ForMember(x => x.educations, otp => otp.Ignore())
                .ForMember(x => x.experiences, opt => opt.Ignore())
                .ForMember(x => x.cv_file_path, opt => opt.Ignore())
                .ForMember(x => x.image_path, opt => opt.Ignore())

                ;
        }
    }
}
