using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.HR
{
    public class EmployeeOfCompanyPagination : PaginationData<EmployeeOfCompanyResponse> { }

    public class EmployeeOfCompanyResponse
    {
        public int id { get; set; }
        public string employee_name  { get; set; }
        public string job_name { get; set; }    
        public string management_name {  get; set; }
        public string nationality_name { get; set; }    
       


    }
}
