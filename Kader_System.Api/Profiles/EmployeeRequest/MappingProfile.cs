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

            CreateMap<LoanRequest, DTOLoanRequest>().ReverseMap();
            CreateMap<CreateTransSalaryIncreaseRequest, TransSalaryIncrease>();
            CreateMap<DTOLeavePermissionRequest, LeavePermissionRequest>().ReverseMap();
            CreateMap<DTOAllowanceRequest, AllowanceRequest>();
            CreateMap<DTODelayPermissionRequest, DelayPermission>();
            CreateMap<DTOVacationRequest, VacationRequests>().ReverseMap();
            CreateMap<DTOCreateLeavePermissionRequest, LeavePermissionRequest>().ReverseMap();
            CreateMap<DTOSalaryIncreaseRequest, SalaryIncreaseRequest>();
            CreateMap<DTOContractTerminationRequest, ContractTerminationRequest>();
            CreateMap<ContractTerminationRequest, ListOfContractTerminationRequestResponse>().ForMember(dest => dest.ApporvalStatus, opt => opt.MapFrom(src => src.StatuesOfRequest != null ? src.StatuesOfRequest.ApporvalStatus : null));
            CreateMap<LoanRequest, DTOLoanRequest>().ReverseMap();
            CreateMap<LoanRequest, ListOfLoanRequestResponse>()
              .ForMember(dest => dest.ApporvalStatus, opt => opt.MapFrom(src => src.StatuesOfRequest != null ? src.StatuesOfRequest.ApporvalStatus : null));
            CreateMap<ResignationRequest, DTOResignationRequest>().ReverseMap();
            CreateMap<ResignationRequest, DtoListOfResignationResposne>().ForMember(dest => dest.ApporvalStatus, opt => opt.MapFrom(src => src.StatuesOfRequest != null ? src.StatuesOfRequest.ApporvalStatus : null));
            CreateMap<VacationRequests, DtoListOfVacationRequestResponse>().
                                                    ForMember(dest => dest.ApporvalStatus, opt => opt.MapFrom(src => src.StatuesOfRequest != null ? src.StatuesOfRequest.ApporvalStatus : null));
            CreateMap<SalaryIncreaseRequest, DTOSalaryIncreaseRequest>().ReverseMap();
            CreateMap<SalaryIncreaseRequest, DTOListOfSalaryIncreaseRepostory>()
              .ForMember(dest => dest.ApporvalStatus, opt => opt.MapFrom(src => src.StatuesOfRequest != null ? src.StatuesOfRequest.ApporvalStatus : null));
            CreateMap<AllowanceRequest, DTOAllowanceRequest>().ReverseMap();
            CreateMap<AllowanceRequest, ListOfAllowanceRequestResponse>()
                         .ForMember(dest => dest.ApporvalStatus, opt => opt.MapFrom(src => src.StatuesOfRequest != null ? src.StatuesOfRequest.ApporvalStatus : null));

            CreateMap<LeavePermissionRequest, ListOfLeavePermissionsRequestResponse>()
              .ForMember(dest => dest.ApporvalStatus, opt => opt.MapFrom(src => src.StatuesOfRequest != null ? src.StatuesOfRequest.ApporvalStatus : null))
              .ForMember(x => x.EmployeeName, d => d.MapFrom(S => S.Employee.SetName()));
            CreateMap<DelayPermission, DtoListOfDelayRequestReponse>()
              .ForMember(dest => dest.ApporvalStatus, opt => opt.MapFrom(src => src.StatuesOfRequest != null ? src.StatuesOfRequest.ApporvalStatus : null)).
                  ForMember(x => x.EmployeeName, x => x.MapFrom(d => d.Employee.SetName()));
            CreateMap<StCreateSubMainScreenRequest, StScreenSub>().ReverseMap();
            CreateMap<StUpdateMainScreenCategoryRequest, StMainScreenCat>().ReverseMap();
            CreateMap<StCreateMainScreenRequest, StMainScreen>().ReverseMap();
        }
    }
}
