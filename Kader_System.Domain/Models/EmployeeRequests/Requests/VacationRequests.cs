namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Table("Hr_VacationRequests")]
    public class VacationRequests
    {
        [Key]
        public int Id { get; set; }
        public int DayCounts { get; set; }
        public DateTime StartDate { get; set; }

        public string? Notes { get; set; }
        public string? AttachmentFileName { get; set; }

        public int VacationTypeId { get; set; }
        [ForeignKey(nameof(VacationTypeId))]
        public virtual HrVacationType VacationType { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual HrEmployee Employee { get; set; }
        public string? StatusMessage { get; set; }
        public int? ApporvalStatus { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy { get; set; }
    }
}
