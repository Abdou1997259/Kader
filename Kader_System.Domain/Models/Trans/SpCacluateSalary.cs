using Microsoft.EntityFrameworkCore;

namespace Kader_System.Domain.Models.Trans;

[Keyless]
public class SpCacluateSalary
{


    public int? EmployeeId { get; set; }

    public string? FullNameAr { get; set; }
    public string? FullNameEn { get; set; }
    public double? AccommodationAllowance { get; set; }

    public double? CalculatedSalary { get; set; }
    public double? FixedSalary { get; set; }
    //public override bool Equals(object? obj)
    //{
    //    if (obj == null || !(obj is SpCacluateSalary))
    //    {
    //        return false;
    //    }

    //    SpCacluateSalary other = (SpCacluateSalary)obj;

    //    return this.EmployeeId == other.EmployeeId &&
    //           this.FullNameAr == other.FullNameAr &&
    //           this.FullNameEn == other.FullNameEn &&
    //           this.AccommodationAllowance == other.AccommodationAllowance &&
    //           this.CalculatedSalary == other.CalculatedSalary &&
    //           this.FixedSalary == other.FixedSalary;
    //}
    //public override int GetHashCode()
    //{
    //    unchecked // Overflow is fine, just wrap
    //    {
    //        int hash = 17; // Prime number to start with

    //        hash = hash * 23 + EmployeeId.GetHashCode();
    //        hash = hash * 23 + (FullNameAr?.GetHashCode() ?? 0);
    //        hash = hash * 23 + (FullNameEn?.GetHashCode() ?? 0);
    //        hash = hash * 23 + AccommodationAllowance.GetHashCode();
    //        hash = hash * 23 + CalculatedSalary.GetHashCode();
    //        hash = hash * 23 + FixedSalary.GetHashCode();

    //        return hash;
    //    }
    //}



}

