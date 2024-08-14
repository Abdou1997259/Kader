using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.EmployeesRequests
{


    public class GetSalaryIncreseRequestResponse : PaginationData<DTOListOfSalaryIncreaseRepostory>
    {



    }
    public class DTOListOfSalaryIncreaseRepostory 
    {
        public int EmployeeId { get; set; }
    
        public double Amount { get; set; }
        public int? ApporvalStatus { get; set; }

        public string? Notes { get; set; }
 

    }
}
