namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CalcluateEmpolyeeFilters
    {
        public int? EmployeeId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int StartActionDay { get; set; }
        public DateOnly StartCalculationDate { get; set; }
        public int? CompanyId { get; set; }
        public int? ManagerId { get; set; }
        public int? DepartmentId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly DocumentDate { get; set; }

    }
}
