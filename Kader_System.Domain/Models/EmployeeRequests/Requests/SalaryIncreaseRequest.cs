using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Table("Hr_SalaryIncreaseRequest")]

    public class SalaryIncreaseRequest : BaseEntity
    {
        [Key]
        public int Id { get; set; }
    
        public double  Amount { get; set; }
        public string? AttachmentFileName { get; set; }
        public string? Notes { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public HrEmployee employee { get; set; }

        public StatuesOfRequest? StatuesOfRequest { get; set; }


    }
}
