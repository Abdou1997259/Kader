using Microsoft.EntityFrameworkCore;

namespace Kader_System.Domain.Models.Trans
{
    [Keyless]
    public class SpCaclauateSalaryDetailedTrans
    {
        public int TransId { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly JournalDate { get; set; }
        public JournalType JournalType { get; set; }
        public double CalculatedSalary { get; set; }
        public int? CacluateSalaryId { get; set; }

    }
}
