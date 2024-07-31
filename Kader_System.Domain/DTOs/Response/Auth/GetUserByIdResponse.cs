using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.Auth
{
    public class GetUserByIdResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
        public int FinancialYear { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
      
        public List<int>TitleId { get; set; }
        public int JobTitle { get; set; }
    }
}
