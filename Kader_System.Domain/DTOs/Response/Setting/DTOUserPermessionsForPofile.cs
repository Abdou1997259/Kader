using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.Setting
{
    public class DTOUserPermessionsForPofile
    {
        public int sub_id {  get; set; }
        public int cat_id {  get; set; }
        public int main_id {  get; set; }
        public string sub_title { get; set; }
        public string cat_title { get; set; }
        public string main_title { get; set; }
        public string screen_code { get; set; }
        public string actions { get; set; }
        public int [] permissions { get; set; }
    }
}
