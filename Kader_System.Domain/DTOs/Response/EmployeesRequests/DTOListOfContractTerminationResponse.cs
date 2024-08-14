using Kader_System.Domain.Models.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.EmployeesRequests
{
    public class GetAllContractTermiantionResponse:PaginationData<DTOListOfContractTerminationResponse>
    {

    }
    public class DTOListOfContractTerminationResponse
    {
        public int Id { get; set; }
        public string? Notes { get; set; }
        public int? ApporvalStatus { get; set; }

        public int EmployeeId { get; set; }
   
    }
}
