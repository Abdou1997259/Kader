using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests
{
    public class DTOLoanRequest
    {
        public int EmployeeId { get; set; }
        public int InstallmentsCount { get; set; }
        public double Amount { get; set; }
        public string? AtachmentPath { get; set; }
        public string? Notes { get; set; }




    }
}
