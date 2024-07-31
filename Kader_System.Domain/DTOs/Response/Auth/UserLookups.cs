using Kader_System.Domain.DTOs.Request.Trans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.Auth
{
    public class UsersLookups
    {
        public IEnumerable<CompanyLookup> Companies { get; set; }
        public IEnumerable<JobsLookups> Jobs { get; set; }
        public IEnumerable<int> CompanyYear { get; set; }
        public IEnumerable<TitleLookups> Titles { get; set; }
    }
    public class JobsLookups
    {
        public int Id { get; set; }
        public string JobName { get; set; }    
  
    }
    public class TitleLookups { 
    
      public int Id { get; set;}
      public string TitleName { get; set; }   

    
    }
}
