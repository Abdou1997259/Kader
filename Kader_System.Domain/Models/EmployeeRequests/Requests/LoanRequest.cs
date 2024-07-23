using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Table("hr_loan_request")]

    public class LoanRequest : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int InstallmentsCount { get; set; }
        public DateOnly  StartDate { get; set; }
        public double  Amount { get; set; }
        public string? AttachmentFileName { get; set; }
        public string? Notes { get; set; }

        public int? EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual HrEmployee? Employee { get; set; }

        public StatuesOfRequest? StatuesOfRequest { get; set; }

    }
}
