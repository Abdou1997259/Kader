using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.HR
{

    public class CompanyResponse
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<ManagementResponse> Children { get; set; }
    }

    public class ManagementResponse
    {
        public int ManagementId { get; set; }
        public string ManagementName { get; set; }
        public List<DepartmentResponse> Children { get; set; }
    }

    public class DepartmentResponse
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<EmployeeResponse> Children { get; set; }
    }

    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }

}
