using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.HR
{
   
    public class CompanyResponse
    {
        public int? Id { get; set; }
     public string Name { get; set; }
        public IEnumerable<ManagementResponse> Managements { get; set; }
    }

    public class ManagementResponse
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? CompanyId { get; set; }
        public int? ManagerId { get; set; }
        public string ManagerName { get; set; }
        public List<DepartmentResponse> Departments { get; set; }
    }

    public class DepartmentResponse
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<EmployeeResponse> Employees { get; set; }
    }

    public class EmployeeResponse
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
