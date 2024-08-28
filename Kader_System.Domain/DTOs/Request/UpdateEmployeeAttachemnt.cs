using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request
{
    public class UpdateEmployeeAttachemnt
    {
        public IFormFile? employee_attachment { get; set; } = default!;
    }
}
