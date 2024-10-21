namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class GetSalariesEmployeeResponse
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public double HousingAllownces { get; set; }
        public int WrokingDay { get; set; }
        public double BasicSalary { get; set; }
        public double AccommodationAllowance { get; set; }
        public IEnumerable<AdditionalValues> AdditionalValues { get; set; }
        //public Header Headers { get; set; }

        public MinuesValues MinuesValues { get; set; }

        public IEnumerable<Absent> Absents { get; set; }
        public DisbursementType DisbursementType { get; set; }
        public double HousingAllowances { get; set; }
        public int WorkingDay { get; set; }
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
        public int? Id { get; set; }
        public double? Value { get; set; }
    }
    public class Deduction
    {
        public int? Id { get; set; }
        public double? Value { get; set; }

    }
    public class AdditionalValues
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public double? Value { get; set; }
    }

    public class Header
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string WorkedDays { get; set; }
        public string Fixed { get; set; }
        public string TotalAdditionalValues { get; set; }
        public string TotalMinues { get; set; }
        public string TotalAll { get; set; }
        public string PaymentMethod { get; set; }
        public string[] Absent { get; set; }
        public List<string> AdditionalValues { get; set; }
        public string ScreenName { get; set; }
        public List<string> MinuesValues { get; set; }
    }
}
