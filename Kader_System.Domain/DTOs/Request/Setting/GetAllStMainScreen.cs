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
        public required string Screen_main_title { get; set; }
        public required string? Screen_main_image { get; set; }
        public List<GetAllStMainScreenCat> CategoryScreen { get; set; }
    }
}
