using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response;
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
            CreateMap<DTOListOfLoanRequestResponse, LoanRequest>().ReverseMap();
            CreateMap<CreateTransSalaryIncreaseRequest, TransSalaryIncrease>();
            CreateMap<DTOLeavePermissionRequest, LeavePermissionRequest>().ReverseMap();
            CreateMap<DTOAllowanceRequest, AllowanceRequest>();
            CreateMap<DTODelayPermissionRequest, DelayPermission>();
            CreateMap<DTOVacationRequest, VacationRequests>().ReverseMap();
            CreateMap<DTOCreateLeavePermissionRequest,LeavePermissionRequest>().ReverseMap();
            CreateMap<DTOSalaryIncreaseRequest, SalaryIncreaseRequest>();
            CreateMap<DTOContractTerminationRequest, ContractTerminationRequest>();
            CreateMap<ContractTerminationRequest, DTOListOfContractTerminationResponse>().ReverseMap();
            CreateMap<LoanRequest, DTOLoanRequest>().ReverseMap();
            CreateMap<LoanRequest, DTOListOfLoanRequestResponse>().ReverseMap();
            CreateMap<ResignationRequest, DTOResignationRequest>().ReverseMap();
            CreateMap<ResignationRequest, DtoListOfResignationResposne>().ReverseMap();
            CreateMap<VacationRequests, DtoListOfVacationRequestResponse>().ReverseMap();
            CreateMap<SalaryIncreaseRequest, DTOSalaryIncreaseRequest>().ReverseMap();
            CreateMap<SalaryIncreaseRequest, DTOListOfSalaryIncreaseRepostory>().ReverseMap();
            CreateMap<AllowanceRequest, DTOAllowanceRequest>().ReverseMap();
            CreateMap<AllowanceRequest, DTOAllowanceRequestResponse>().ReverseMap();
            CreateMap<LeavePermissionRequest, ListOfLeavePermissionsReponse>().ForMember(x => x.EmployeeName, d => d.MapFrom(S => S.Employee.SetName()));
            CreateMap<DelayPermission, DtoListOfDelayRequestReponse>().ForMember(x => x.EmployeeName, x => x.MapFrom(d => d.Employee.SetName()));
            CreateMap<StCreateSubMainScreenRequest, StScreenSub>().ReverseMap();
            CreateMap<StUpdateMainScreenCategoryRequest, StMainScreenCat>().ReverseMap();
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Screen_main_id));

        }
    }
}
