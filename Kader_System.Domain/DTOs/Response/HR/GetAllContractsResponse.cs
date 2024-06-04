namespace Kader_System.Domain.DTOs.Response.HR
{
    public class GetAllContractsResponse:PaginationData<ContractData>
    {
        
    }

    public class ContractData
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
        public string AddedByUser { get; set; }
        public List<GetAllContractDetailsResponse>? Details { get; set; } = [];
    }


    public class GetAllContractDetailsResponse
    {
        public int Id { get; set; }
        public int? AllowanceId { get; set; }
        public string? AllowanceName { get; set; }
        public double? Value { get; set; }
        public bool? IsPercent { get; set; }
        public RowStatus Status { get; set;}
    }


}
