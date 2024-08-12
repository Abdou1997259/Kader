using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.Setting
{
    public class DTOSPGetUserPermissionsBySubScreen
    {
        public int cat_id { get; set; }
        public string cat_title { get; set; }
        public int main_id { get; set; }
        public string main_img { get; set; }
        public string main_title { get; set; }
        public string screen_code { get; set; }
        public int sub_id { get; set; }
        public string sub_title { get; set; }
        public int TitleId {  get; set; }
        public string actions { get; set; }
        public Dictionary<string,bool> permissions { get; set; }
        public string? url { get; set; }

    }
}
