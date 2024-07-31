using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.Setting
{
    public class GetAllStMainScreenCat
    {
        public List<int> Ids { get; set; }
        //public required List<string> Screen_cat_title_en { get; set; }
        public required List<string> Screen_cat_title_ar { get; set; }
        public required List<IFormFile> Screen_main_cat_image { get; set; }
        public List<GetAllStScreenSub> StScreenSub { get; set; } = [];
    }
}
