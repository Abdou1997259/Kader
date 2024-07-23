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
            CreateMap<DTOListOfLoanRequestResponse, LoanRequest>().ReverseMap();
            CreateMap<CreateTransSalaryIncreaseRequest, TransSalaryIncrease>();
            CreateMap<DTOLeavePermissionRequest, LeavePermissionRequest>();
            CreateMap<DTOAllowanceRequest, AllowanceRequest>();
            CreateMap<DTODelayPermissionRequest, DelayPermission>();
            CreateMap<DTOVacationRequest, VacationRequests>().ReverseMap();
            CreateMap<DTOSalaryIncreaseRequest, SalaryIncreaseRequest>();
            CreateMap<DTOContractTerminationRequest, ContractTerminationRequest>();
            CreateMap<ContractTerminationRequest, DTOListOfContractTerminationResponse>().ReverseMap();
            CreateMap<LoanRequest, DTOLoanRequest>().ReverseMap();
            CreateMap<LoanRequest, DTOListOfLoanRequestResponse>().ReverseMap();
            CreateMap<ResignationRequest, DTOResignationRequest>().ReverseMap();
            CreateMap<ResignationRequest, DtoListOfResignationResposne>().ReverseMap();
            CreateMap<VacationRequests, DtoListOfVacationRequestResponse>().ReverseMap();
            CreateMap<SalaryIncreaseRequest, DTOSalaryIncreaseRequest>().ReverseMap();
            CreateMap<AllowanceRequest, DTOAllowanceRequest>().ReverseMap();
            
        }
    }
}
