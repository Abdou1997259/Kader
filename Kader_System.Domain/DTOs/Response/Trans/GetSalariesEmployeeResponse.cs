

namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class GetSalariesEmployeeResponse
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public int WrokingDay { get; set; }
        public double BasicSalary { get; set; }
        public double AccommodationAllowance { get; set; }
        public IEnumerable<AddtionalValues> AddtionalValues { get; set; }

        public MinuesValues MinuesValues { get; set; }

        public IEnumerable<Absent> Absents { get; set; }
        public DisbursementType DisbursementType { get; set; }





    }
    public enum DisbursementType
    {
        BankingType = 1,
        CachingType
    }
    public class Absent
    {
        public double? Days { get; set; }
        public double? Sum { get; set; }
    }
    public class MinuesValues
    {
        public IEnumerable<Loan> Loans { get; set; }
        public IEnumerable<Deduction> Deductions { get; set; }
    }

    public class Loan
    {
        public int Id { get; set; }
        public double Value { get; set; }
    }
    public class Deduction
    {
        public int Id { get; set; }
        public double Value { get; set; }

    }
    public class AddtionalValues
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }


}
