using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Models.HR
{
    [Table("hr_Employee_attendance")]
    public class HrEmployeeAttendance
    {
        public int Id { get; set; } 
        public DateOnly AttendanceDate { get; set; }
        public TimeOnly CheckIn { get; set; }
        public TimeOnly CheckOut { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public HrEmployee Employee { get; set; }
    }
}
