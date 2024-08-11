using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Models.Setting
{
    public class SpGetAllScreens
    {
        public int main_id { get; set; }
        public string? main_title { get; set; }
        public int cat_id { get; set; }
        public string? cat_title { get; set; }
        public string? Screen_main_cat_image { get; set; }
        public int sub_id { get; set; }
        public string? sub_title { get; set; }
        public string? screen_code { get; set; }
        public string? url { get; set; }
        public StMainScreen mainScreen { get; set; }
        public StMainScreenCat catscreen { get; set; }


    }
  
}
