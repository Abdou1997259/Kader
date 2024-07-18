using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.HR
{
    public class AddEmpolyeeToDepartmentRequest
    {
        public int EmpolyeeId { get; set; }
        public int DepartmentId { get; set; }
        public int MangamentId { get; set; }

    }
}
