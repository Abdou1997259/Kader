namespace Kader_System.Domain.DTOs.Response.HR
{
    public class GetAllEmployeeNotesResponse : PaginationData<EmployeeNotesData>
    {

    }

    public class EmployeeNotesData
    {
        public int Id { get; set; }
        public DateOnly added_date { get; set; }
        public string AddedBy { get; set; }
        public int employee_id { get; set; }
        public string employee_name { get; set; } = string.Empty;
        public string notes { get; set; } = string.Empty;
        public string user_image_url { get; set; }
    }
}
