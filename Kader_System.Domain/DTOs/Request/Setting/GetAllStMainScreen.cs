using Kader_System.Domain.DTOs.Response.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.Setting
{
    public class GetMyProfilePermissionAndScreen
    {
        public IEnumerable<GetAllStMainScreen> getAllStMainScreens { get; set; }
        public List<Dictionary<string, GetUserPermission>>?  myPermissions { get; set; }
    }
    public class MyPermission
    {
     
        public Dictionary<string, IEnumerable<GetUserPermission>> GetUserPermission {  get; set; }

    }
    public class GetAllStMainScreen
    {
        public int Id { get; set; }
         public required string main_title { get; set; }
        public required string? main_image { get; set; }
        public IEnumerable<GetAllStMainScreenCat> cats { get; set; }
       
    }
}
