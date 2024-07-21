using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.Services.Services
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTransSalaryIncreaseRequest, TransSalaryIncrease>();
            CreateMap<DTOLeavePermissionRequest, LeavePermissionRequest>();
            CreateMap<DTOAllowanceRequest, AllowanceRequest>();
            CreateMap<DTODelayPermissionRequest, DelayPermission>();
            CreateMap<DTOVacationRequest, VacationRequests>();
            CreateMap<DTOSalaryIncreaseRequest, SalaryIncreaseRequest>();
            CreateMap<DTOContractTerminationRequest, HrContractTermination>();
            CreateMap<LoanRequest, DTOLoanRequest>().ReverseMap();
            CreateMap<LoanRequest, DTOListOfLoanRequestResponse>().ReverseMap();
       
        }
    }
}
