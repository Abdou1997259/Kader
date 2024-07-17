using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Table("Hr_ResignationRequest")]
    public class ResignationRequest :BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]

        public string? AtachmentPath { get; set; }
        public string? Notes { get; set; }

    }
}
