using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.Services.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DTOLeavePermissionRequest, LeavePermissionRequest>()
                       .ForMember(dest => dest.StatusMessage, opt => opt.Ignore())
                       .ForMember(dest => dest.ApporvalStatus, opt => opt.Ignore())
                       .ForMember(dest => dest.ApprovedDate, opt => opt.Ignore())
                       .ForMember(dest => dest.ApprovedBy, opt => opt.Ignore());
            CreateMap<DTOCreateLeavePermissionRequest, LeavePermissionRequest>();
            CreateMap<DTOAllowanceRequest, AllowanceRequest>()
                       .ForMember(dest => dest.StatusMessage, opt => opt.Ignore())
                       .ForMember(dest => dest.ApporvalStatus, opt => opt.Ignore())
                       .ForMember(dest => dest.ApprovedDate, opt => opt.Ignore())
                       .ForMember(dest => dest.ApprovedBy, opt => opt.Ignore());
            CreateMap<DTODelayPermissionRequest, DelayPermission>()
                       .ForMember(dest => dest.StatusMessage, opt => opt.Ignore())
                       .ForMember(dest => dest.ApporvalStatus, opt => opt.Ignore())
                       .ForMember(dest => dest.ApprovedDate, opt => opt.Ignore())
                       .ForMember(dest => dest.ApprovedBy, opt => opt.Ignore());
            CreateMap<DTOVacationRequest, VacationRequests>()
                       .ForMember(dest => dest.StatusMessage, opt => opt.Ignore())
                       .ForMember(dest => dest.ApporvalStatus, opt => opt.Ignore())
                       .ForMember(dest => dest.ApprovedDate, opt => opt.Ignore())
                       .ForMember(dest => dest.ApprovedBy, opt => opt.Ignore());
            CreateMap<DTOSalaryIncreaseRequest, SalaryIncreaseRequest>()
                       .ForMember(dest => dest.StatusMessage, opt => opt.Ignore())
                       .ForMember(dest => dest.ApporvalStatus, opt => opt.Ignore())
                       .ForMember(dest => dest.ApprovedDate, opt => opt.Ignore())
                       .ForMember(dest => dest.ApprovedBy, opt => opt.Ignore());
            CreateMap<DTOContractTerminationRequest, HrContractTermination>()
                       .ForMember(dest => dest.StatusMessage, opt => opt.Ignore())
                       .ForMember(dest => dest.ApporvalStatus, opt => opt.Ignore())
                       .ForMember(dest => dest.ApprovedDate, opt => opt.Ignore())
                       .ForMember(dest => dest.ApprovedBy, opt => opt.Ignore());

            CreateMap<LoanRequest, DTOLoanRequest>().ReverseMap();
            //CreateMap<DTOListOfLoanRequestResponse, LoanRequest>().ReverseMap();


        }
    }
}
