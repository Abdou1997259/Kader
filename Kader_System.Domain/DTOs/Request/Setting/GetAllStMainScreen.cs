using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.Setting
{
    public class GetAllStMainScreen
    {
        public int Id { get; set; }
        //public required string Screen_main_title_en { get; set; }
        public required string Screen_main_title_ar { get; set; }
        public List<GetAllStMainScreenCat> CategoryScreen { get; set; }
    }
}
