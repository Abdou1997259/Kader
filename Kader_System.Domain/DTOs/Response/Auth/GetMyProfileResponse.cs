﻿using Kader_System.Domain.DTOs.Request.Setting;
using Kader_System.Domain.DTOs.Request.Trans;
using Kader_System.Domain.DTOs.Response.HR;
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
        public string  Title{ get; set; }
        public User user { get; set; }

    }
    public class User
    {
        public dynamic Companys { get; set; }
        public int Years { get; set; }
        public int CurrentCompany { get; set; }
        public string CurrentCompanyName { get; set; }  
        public int CurrentYear { get; set; }
        public int CurrentTitles { get; set; }    
        public dynamic Mypermissions {  get; set; }
        public List<GetAllStMainScreen> Screens { get;set; }



    }
    public class Titles
    {
        public int Id { get; set; }
        public string Name { get; set; } 
    
    
    }


}