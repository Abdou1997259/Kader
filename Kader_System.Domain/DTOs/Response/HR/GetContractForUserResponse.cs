

namespace Kader_System.Domain.DTOs.Response.HR
{
    public class GetContractForUserResponse
    {
        public double SalaryFixed { get; set; }
        public double SalaryTotal { get; set; }   
        public string AddedBy { get; set; }
        public string ContractFile { get; set; }    
        public double HousingAllowance { get; set; }
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateOnly StartDate { get; set; } 
        public string EmployeeName { get; set; }
        public DateOnly EndDate { get; set; }   

    }
}
