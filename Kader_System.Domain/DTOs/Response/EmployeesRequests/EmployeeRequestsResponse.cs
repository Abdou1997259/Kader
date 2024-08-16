using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.EmployeesRequests
{
    public class EmployeeRequestsResponse
    {
        public int Id { get; set; }  
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string? requet_date { get; set; }
        public string? Notes { get; set; }
        public int? ApporvalStatus { get; set; }
        public string reason { get; set; }
        public string? AtachmentPath { get; set; }
    }
    public class ListOfLeavePermissionsReponse:EmployeeRequestsResponse
    {
        public TimeOnly LeaveTime { get; set; }
        public TimeOnly? BackTime { get; set; }
    }
    public class ListOfDelayPermissionRequest: EmployeeRequestsResponse
    {
        public int? HoursDelay { get; set; }
    }
}
