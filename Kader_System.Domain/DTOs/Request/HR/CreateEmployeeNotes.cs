using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.HR
{
    public class CreateEmployeeNotes
    {
        public int EmployeeId { get; set; }
        public string notes { get; set; }
         
    }
}
