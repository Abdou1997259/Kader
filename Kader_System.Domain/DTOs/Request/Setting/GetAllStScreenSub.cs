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
        public int? Screen_CatId { get; set; }
        public string cat_Title { get; set; }
        public int main_id { get; set; }
       public string main_title { get; set; }

        public required string sub_title { get; set; }
        public string url { get; set; }
        public string screen_code { get; set; }
        public string sub_image { get; set; }
        public string  actions { get; set; }   
        public string permissions { get; set; }   
 


    }
}
