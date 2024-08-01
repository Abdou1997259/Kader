using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response
{
    public class CreateUserResponse
    {
        public string UserName { get; set; }
      
        public string Email { get; set; }
        public string FullName { get; set; }
        public List<int> TitleId { get; set; }
        public List<int> CompanyId { get; set; }
        public int JobTitle { get; set; }
        public int CompanyYear { get; set; }
        public string Token { get; set; }
    }
}
