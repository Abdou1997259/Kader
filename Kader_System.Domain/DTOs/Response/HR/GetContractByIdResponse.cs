namespace Kader_System.Domain.DTOs.Response.HR
{
    public class GetContractByIdResponse
    {
      public GetContractDataByIdResponse Master { get; set; }
      public object employees { get; set; }
      public object allowances { get; set; }

    }

    public class GetContractDataByIdResponse
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public double TotalSalary { get; set; }
        public double FixedSalary { get; set; }
        public double HousingAllowance { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string ContractFile { get; set; }
        public string FileName { get; set; }
        public string? AddedByUser { get; set; }
        public List<GetAllContractDetailsResponse>? Details { get; set; } = [];
    }

}
