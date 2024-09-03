using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.Setting
{
    public class ScreenLookups
    {
        public List<ScreenMainLookup> main { get; set; }
        public List<ScreenCatLookup> cat { get; set; }
        public List<ScreenSubLookup> sub { get; set; }  

    }
    public class ScreenMainLookup
    {
        public int Id { get; set; }
        public string main_title { get; set; }
        public string main_image { get; set; }
    }
    public class ScreenCatLookup
    {
        public int Id { get; set; }
        public int main_id { get; set; }
        public string cat_title { get; set; }
        public string Main_Title { get; set; }
        public string main_image { get; set; }


    }
    public class ScreenSubLookup
    {
        public int sub_id { get; set; }
        public int cat_id { get; set; }
        public string Cat_title { get; set; }
        public string sub_title { get; set; }
        public string url { get; set; }
        public string screen_code { get; set; }
    }
}