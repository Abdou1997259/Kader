using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.Setting
{
    public class GetAllSubScreenInfo
    {
        public string Actions { get; set; }
        public int SubId { get; set; }
        public int CatId { get;set; }
        public int MainId { get; set; }
        public string CatTitle { get; set; }
         public string ScreenCode { get; set; } 
        public string SubTitle { get; set; }    

        public string Url { get; set; }
        public Permission Permissions { get; set; }

        public string MainImage { get; set; }
        public string MainTitle { get; set; }
       
    }
    public class Permission
    {
        public bool Add { get;set; }
        public bool View { get; set; }
        public bool Delete { get; set; }    
        public bool Edit { get; set; }
      
    }
}
