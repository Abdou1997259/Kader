using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.Setting
{
    public class GetAllStMainScreenCat
    {
        public int Id { get; set; }
        public int MainScreenId { get; set; }


        public required string Screen_cat_title { get; set; }
        public required string Screen_main_cat_image { get; set; }
        public ICollection<GetAllStScreenSub> StScreenSub { get; set; } = [];
    }
}
