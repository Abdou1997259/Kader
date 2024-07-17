using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests
{
    public class DTOSalaryIncreaseRequest
    {
        public int EmployeeId { get; set; }
        public double Amount { get; set; }
        public string? Notes { get; set; }
        public string? AttachmentPath { get; set; }
    }
}
