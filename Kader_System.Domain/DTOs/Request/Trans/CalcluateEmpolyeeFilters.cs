namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CalcluateEmpolyeeFilters
    {


        [DefaultValue(null)]

        public int? EmployeeId { get; set; } = null;
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int StartActionDay { get; set; }
        public DateOnly StartCalculationDate { get; set; }

        [DefaultValue(null)]
        public int? CompanyId { get; set; } = null;

        [DefaultValue(null)]
        public int? ManagerId { get; set; } = null;

        [DefaultValue(null)]
        public int? DepartmentId { get; set; } = null;
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly DocumentDate { get; set; }

        [DefaultValue(null)]
        public string? Description { get; set; }

    }
}
