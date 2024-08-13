using Microsoft.AspNetCore.Mvc;

namespace Kader_System.Domain.DTOs.Request.HR
{
    public class CreateContractRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int employee_id { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public double total_salary { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public double fixed_salary { get; set; }
        public double housing_allowance { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly start_date { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly end_date { get; set; }
        //[Required(ErrorMessage = Annotations.FieldIsRequired)]
        //public string? ContractFile { get; set; }

        public IFormFile? contract_file { get;set; }


      
  
        public List<CreateContractDetailsRequest>? details { get; set; } 

    }

    public class CreateContractDetailsRequest
    {
        public int id { get; set; }
        public int allowance_id { get; set; }
        public double value { get; set; }
        public bool is_percent { get; set; }

        public RowStatus status { get; set; }
    }
}
