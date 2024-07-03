using Microsoft.EntityFrameworkCore;

namespace Kader_System.Domain.Models.Trans;

[Keyless]
public class SpCacluateSalary
{


    public int EmployeeId { get; set; }
    public double CalculatedSalary { get; set; }
    public double TotalSalary { get; set; }

}

