using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.Services.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DTOLeavePermissionRequest, LeavePermissionRequest>()

                       .ForMember(dest => dest.StatuesOfRequest.StatusMessage, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApporvalStatus, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApprovedDate, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApprovedBy, opt => opt.Ignore());
            CreateMap<DTOCreateLeavePermissionRequest, LeavePermissionRequest>();
            CreateMap<DTOAllowanceRequest, AllowanceRequest>()
                       .ForMember(dest => dest.StatuesOfRequest.StatusMessage, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApporvalStatus, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApprovedDate, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApprovedBy, opt => opt.Ignore());
            CreateMap<DTODelayPermissionRequest, DelayPermission>()
                       .ForMember(dest => dest.StatuesOfRequest.StatusMessage, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApporvalStatus, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApprovedDate, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApprovedBy, opt => opt.Ignore());
            CreateMap<DTOVacationRequest, VacationRequests>()
                       .ForMember(dest => dest.StatuesOfRequest.StatusMessage, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApporvalStatus, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApprovedDate, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApprovedBy, opt => opt.Ignore());
            CreateMap<DTOSalaryIncreaseRequest, SalaryIncreaseRequest>()
                       .ForMember(dest => dest.StatuesOfRequest.StatusMessage, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApporvalStatus, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApprovedDate, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApprovedBy, opt => opt.Ignore());
            CreateMap<DTOContractTerminationRequest, HrContractTermination>()
                       .ForMember(dest => dest.StatuesOfRequest.StatusMessage, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApporvalStatus, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApprovedDate, opt => opt.Ignore())
                       .ForMember(dest => dest.StatuesOfRequest.ApprovedBy, opt => opt.Ignore());

            CreateMap<LoanRequest, DTOLoanRequest>().ReverseMap();
            CreateMap<DTOListOfLoanRequestResponse, LoanRequest>().ReverseMap();
            CreateMap<CreateTransSalaryIncreaseRequest, TransSalaryIncrease>();
            CreateMap<DTOLeavePermissionRequest, LeavePermissionRequest>();
            CreateMap<DTOAllowanceRequest, AllowanceRequest>();
            CreateMap<DTODelayPermissionRequest, DelayPermission>();
            CreateMap<DTOVacationRequest, VacationRequests>().ReverseMap();
            CreateMap<DTOSalaryIncreaseRequest, SalaryIncreaseRequest>();
            CreateMap<DTOContractTerminationRequest, HrContractTermination>();
            CreateMap<LoanRequest, DTOLoanRequest>().ReverseMap();
            CreateMap<LoanRequest, DTOListOfLoanRequestResponse>().ReverseMap();
            CreateMap<ResignationRequest, DTOResignationRequest>().ReverseMap();
            CreateMap<ResignationRequest, DtoListOfResignationResposne>().ReverseMap();
            CreateMap<VacationRequests, DtoListOfVacationRequestResponse>().ReverseMap();
        }
    }
}
