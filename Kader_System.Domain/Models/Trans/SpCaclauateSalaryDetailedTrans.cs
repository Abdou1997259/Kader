using Microsoft.EntityFrameworkCore;

namespace Kader_System.Domain.Models.Trans
{
    [Keyless]
    public class SpCaclauateSalaryDetailedTrans
    {
        public int TransId { get; set; }
        public int EmployeeId { get; set; }
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
        public DateOnly JournalDate { get; set; }

        public JournalType JournalType { get; set; }
        public double? AccommodationAllowance { get; set; }
        public double CalculatedSalary { get; set; }
        public int? CacluateSalaryId { get; set; }
        public double? VacationSum { get; set; }





        public double? VacationDayCount { get; set; }





    }
}
