

using Microsoft.EntityFrameworkCore;

namespace Kader_System.Domain.Models.Trans
{
    [Keyless]
    public class SpCaclauateSalaryDetails
    {
        public int? EmployeeId { get; set; }
        public JournalType? JournalType { get; set; }
        public double? CalculatedSalary { get; set; }


    }
}
