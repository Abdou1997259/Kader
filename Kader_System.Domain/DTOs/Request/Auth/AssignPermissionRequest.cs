using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.Auth
{
    public class AssignPermissionRequest
    {
        public List<int> Permission { get; set; }   
        public int SubScreenId { get;set; }
    }
}
