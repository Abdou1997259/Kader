

namespace Kader_System.Domain.Models.Trans
{
    [Table("Cacluate_Salary")]
    public class CacluateSalary : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public double Salary { get; set; }


        [ForeignKey(nameof(EmployeeId))]

        public HrEmployee Employee { get; set; }

        public int EmployeeId { get; set; }
        public DateOnly ActionDate { get; set; }

    }
}
