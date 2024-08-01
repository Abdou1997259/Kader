using Kader_System.Domain.DTOs.Response.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.Auth
{
    public class GetMyProfileResponse
    {

        public string ApiToken { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Image { get; set; }
        public List<string>  Titles { get; set; }
        public User user { get; set; }

    }
    public class User
    {
        public int Companys { get; set; }
        public int Years { get; set; }
        public int CurrentCompany { get; set; }
        public string CurrentCompanyName { get; set; }  
        public int CurrentYear { get; set; }
        public int CurrentTitles { get; set; }    
        public List<DTOUserPermessionsForPofile> Mypermissions {  get; set; }
        public List<GetMainScreensWithRelatedDataResponse> Screens { get;set; }



    }
    public class Titles
    {
        public int Id { get; set; }
        public string Name { get; set; } 
    
    
    }


}
