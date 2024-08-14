using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class EmployeeWithSalary
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public double Salary
        {
            get; set;
        }
    }
}
