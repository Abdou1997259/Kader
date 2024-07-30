using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.Setting
{
    public class GetMainScreensWithRelatedDataResponse : PaginationData<MainScreenWithCatSubScreens>
    {

    }

    public class MainScreenWithCatSubScreens
    {
        public int Id { get; set; }
        public  string Screen_main_title_en { get; set; }
        public  string Screen_main_title_ar { get; set; }

        public List<GetCatWithSubScreens> CatWithSubScreens { get; set; }

        public class GetCatWithSubScreens
        {
            public int Id { get; set; }
            public string Screen_cat_title_ar { get; set; }
            public string Screen_cat_title_en { get; set; }

            public List<GetSubScreens> GetSubScreens  { get; set; }

        }

        public class GetSubScreens
        {
            public int Id { get; set; }
            public string Screen_sub_title_ar { get; set; }
            public string Screen_sub_title_en { get; set; }

        }






    }
}
