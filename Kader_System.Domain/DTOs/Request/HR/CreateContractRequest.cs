using Microsoft.AspNetCore.Mvc;

namespace Kader_System.Domain.DTOs.Request.HR
{
    public class CreateContractRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public double TotalSalary { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public double FixedSalary { get; set; }
        public double HousingAllowance { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly EndDate { get; set; }
        //[Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string? ContractFile { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string FileName { get; set; }
        public List<CreateContractDetailsRequest>? Details { get; set; } 

    }

    public class CreateContractDetailsRequest
    {
        public int Id { get; set; }
        public int AllowanceId { get; set; }
        public double Value { get; set; }
        public bool IsPercent { get; set; }

        public RowStatus Status { get; set; }
    }
}
