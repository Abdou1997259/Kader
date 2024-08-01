using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.Setting
{
    public class GetAllStScreenSub
    {
        public List<int> Ids { get; set; }
         public required List<string> Screen_sub_title { get; set; }
        public List<string>? Url { get; set; }
        public List<string> ScreenCode { get; set; }
        public List<string>? Screen_sub_image { get; set; }
 


    }
}
