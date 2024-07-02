
namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CalcluateSalaryModelRequest
    {
        public int EmployeeId { get; set; }
        public DateOnly StartActionDate { get; set; }
        public int CompanyId { get; set; }
        public DateOnly DocumentDate { get; set; }
        public bool IsMigrated { get; set; }

    }
}
