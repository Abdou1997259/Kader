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
        public DateTime  StartDate { get; set; }
        public double  Amount { get; set; }
        public string? AtachmentPath { get; set; }
        public string? Notes { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual HrEmployee Employee { get; set; }
        public int Status { get; set; }
        public string? StatusMessage { get; set; }
        public int? ApporvalStatus { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy { get; set; }
    }
}
