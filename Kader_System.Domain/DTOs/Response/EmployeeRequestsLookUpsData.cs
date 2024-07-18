using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response
{
    public class EmployeeRequestsLookUpsData
    {
        public object[] employees { get; set; }
        public object[] allowances { get; set; }
        public object[] vacation_types { get; set; }

    }
}
