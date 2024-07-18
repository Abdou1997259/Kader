
namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CalcluateSalaryModelRequest
    {
        public List<int> EmployeeIds { get; set; }

        public int StartActionDay { get; set; }
        public DateOnly StartCalculationDate { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly DocumentDate { get; set; }

     


    }
}
