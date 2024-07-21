

namespace Kader_System.Domain.DTOs.Response.EmployeesRequests
{
    public class GetAllLoanRequestResponse :PaginationData<DTOListOfLoanRequestResponse> { 
    
    
    
    }
    public class DTOListOfLoanRequestResponse
    {

        public int EmployeeId { get; set; }

        public int InstallmentsCount { get; set; }

        public double Amount { get; set; }


        public string? Notes { get; set; }


    }
}
