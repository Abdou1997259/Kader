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
        public int? main_id { get; set; }
        public required string? main_image { get; set; }


        public required string title { get; set; }
        //public required string image { get; set; }
        public IEnumerable<GetAllStScreenSub> subs { get; set; } = [];
    }
}
