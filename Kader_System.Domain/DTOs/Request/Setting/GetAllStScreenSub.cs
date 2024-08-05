using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.Setting
{
    public class GetAllStScreenSub
    {
        public int Sub_Id{ get; set; }
        public int Screen_CatId { get; set; }
        public string Cat_Title { get; set; }
        public int Screen_MainId { get; set; }
       public string Main_title { get; set; }

        public required string Screen_sub_title { get; set; }
        public string? Url { get; set; }
        public string ScreenCode { get; set; }
        public string? Screen_sub_image { get; set; }
        public string ? Actions { get; set; }   
        public string ? Permissions { get; set; }   
 


    }
}
