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
        public required List<string> Screen_sub_title_en { get; set; }
        public required List<string> Screen_sub_title_ar { get; set; }
        public string Url { get; set; }
    }
}
