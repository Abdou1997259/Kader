using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.Domain.DTOs.Response
{
    public class GetAllLeavePermissionRequestResponse : PaginationData<ListOfLeavePermissionsReponse>
    {
    }

    public class ListOfLeavePermissionsReponse
    {
        public int Id { get; set; }
        public string? AtachmentPath { get; set; }
        public string? Notes { get; set; }
        public double DelayHours { get; set; }
        public int EmployeeId { get; set; }

        public  string EmployeeName { get; set; }

  


    }
}
