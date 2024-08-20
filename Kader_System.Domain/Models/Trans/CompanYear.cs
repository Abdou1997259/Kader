
using Microsoft.AspNetCore.SignalR;

namespace Kader_System.Domain.Models.Trans
{
    
    public class CompanyYear
    {
        public int Id { get; set; }
        public string FinancialYear { get; set; }
   

        public ICollection<ApplicationUser> Users { get; set; }=new HashSet<ApplicationUser>();
     

    }
}
