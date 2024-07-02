using Microsoft.EntityFrameworkCore;

namespace Kader_System.Domain.Models.Trans;

[Keyless]
public class SpCacluateSalary
{

    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public JournalType JournalType { get; set; }
    public double CalculatedSalary { get; set; }

    public DateOnly JournalDate { get; set; }
    public int? CacluateSalaryId { get; set; }
}

