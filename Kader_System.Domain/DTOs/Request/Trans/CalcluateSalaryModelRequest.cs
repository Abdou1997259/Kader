
namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CalcluateSalaryModelRequest
    {
        public int? EmployeeId { get; set; }
        public int StartActionDay { get; set; }
        public DateOnly StartCalculationDate { get; set; }
        public int? CompanyId { get; set; }
        public int? ManagerId { get; set; }
        public DateOnly DocumentDate { get; set; }

        public bool IsMigrated { get; set; }

    }
}
