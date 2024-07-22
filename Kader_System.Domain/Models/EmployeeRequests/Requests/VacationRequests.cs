namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Table("hr_vacation_requests")]
    public class VacationRequests:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int DayCounts { get; set; }
        public DateOnly StartDate { get; set; }

        public string? Notes { get; set; }
        public string? AttachmentFileName { get; set; }

        public int VacationTypeId { get; set; }
        [ForeignKey(nameof(VacationTypeId))]
        public virtual HrVacationType VacationType { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual HrEmployee Employee { get; set; }

        public StatuesOfRequest StatuesOfRequest { get; set; }

    }
}
