using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.Auth
{
    public class Permissions
    {
        public List<int> TitlePermssion { get; set; }   
        public int SubId { get;set; }
    }
}
