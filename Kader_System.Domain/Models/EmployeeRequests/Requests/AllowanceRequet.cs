using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Table("hr_allowance_requet")]
    public class AllowanceRequet
    {
        public int Id { get; set; }
        public string allowance_name { get; set; }


    }
}
