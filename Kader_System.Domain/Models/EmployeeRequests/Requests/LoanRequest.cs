using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Table("Hr_LoanRequest")]

    public class LoanRequest : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int InstallmentsCount { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public DateTime  StartDate { get; set; }
        public double  Amount { get; set; }
        public string? AtachmentPath { get; set; }
        public string? Notes { get; set; }
        public HrEmployee employee { get; set; }

    }
}
