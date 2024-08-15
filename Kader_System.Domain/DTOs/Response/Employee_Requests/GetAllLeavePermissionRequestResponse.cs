using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.Domain.DTOs.Response
{
    public class GetAllLeavePermissionRequestResponse : PaginationData<ListOfLeavePermissionsReponse>
    {
    }

    public class ListOfLeavePermissionsReponse
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? requet_date { get; set; }
        public TimeOnly LeaveTime { get; set; }
        public TimeOnly? BackTime { get; set; }
        public string? Notes { get; set; }
        public int EmployeeId { get; set; }
        public int? ApporvalStatus { get; set; }

        public string? AtachmentPath { get; set; }


    }
}
