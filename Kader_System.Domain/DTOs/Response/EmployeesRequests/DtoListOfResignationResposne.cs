using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.EmployeesRequests
{
    public class GetAllResignations : PaginationData<DtoListOfResignationResposne>
    {



    }
    public class DtoListOfResignationResposne
    {
        public int EmployeeId { get; set; }
        public string? Notes { get; set; }
        public int? ApporvalStatus { get; set; }

        public string? AttachmentPath { get; set; }
    }
}
