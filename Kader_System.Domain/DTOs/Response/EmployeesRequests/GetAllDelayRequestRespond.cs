using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.EmployeesRequests
{
    public class GetAllDelayRequestRespond : PaginationData<DtoListOfDelayRequestReponse>
    {

    }

    public class DtoListOfDelayRequestReponse
    {
        public int Id { get; set; }
        public string EmployeeName{ get; set; }
        public int Amount { get; set; }
        public string? Notes { get; set; }
        public string? Atachment { get; set; }
        public double HoursDelay { get; set; }




    }

}
