using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.EmployeesRequests
{
    public class GetAllowanceRequestResponse : PaginationData<DTOAllowanceRequestResponse>
    {

    }

    public  class DTOAllowanceRequestResponse 
    {
        public int EmployeeId { get; set; }
        public int amount { get; set; }
        public string? Notes { get; set; }

        public string? Atachment { get; set; }




    }


}
