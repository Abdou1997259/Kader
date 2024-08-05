using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.Auth
{
    public class CreateUserRequest
    {


        public string user_name { get; set; }
       
        public string? password { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string full_name { get; set; }
        public int? current_title { get; set; } = 1;
        public int? current_company { get; set; } = 3;
        public List<int>? title_id {  get; set; }=new List<int>() { 1 };
        public List<int> company_id { get; set; } = new List<int>() { 3};
        public  int job_title { get; set; }
        public int financial_year { get; set; }
        public IFormFile? image { get; set; }   
        
        public bool is_active { get;set; }
    }
}
