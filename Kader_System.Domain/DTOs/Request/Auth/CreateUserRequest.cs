using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.Auth
{
    public class CreateUserRequest
    {


        public string UserName { get; set; }
       
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public List<int> TitleId {  get; set; }
        public int CompanyId { get; set; }
        public  int JobTitle { get; set; }
        public int CompanyYear { get; set; }
        public IFormFile? Image { get; set; }   
        
        public bool IsActive { get;set; }
    }
}
