using Microsoft.EntityFrameworkCore;

namespace Kader_System.Domain.Models.Trans
{
    [Keyless]
    public class SpCaclauateSalaryDetailedTrans
    {
        public int TransId { get; set; }
        public int EmployeeId { get; set; }
        public string? FullNameAr { get; set; }
        public string? FullNameEn { get; set; }
        public string? TransNameAr { get; set; }
        public string? TransNameEn { get; set; }
        public DateOnly JournalDate { get; set; }
        public JournalType JournalType { get; set; }
        public double? AccommodationAllowance { get; set; }
        public double CalculatedSalary { get; set; }
        public int? CalculateSalaryId { get; set; }
        public int? CalculateSalaryDetailsId { get; set; }
    }
}
