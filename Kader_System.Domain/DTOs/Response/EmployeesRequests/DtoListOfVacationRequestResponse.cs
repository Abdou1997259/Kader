

namespace Kader_System.Domain.DTOs.Response.EmployeesRequests
{
    public class DtoListOfVacationRequestResponse
    {
       
        public int Id { get; set; }
        public int DayCounts { get; set; }
        public DateOnly StartDate { get; set; }

        public string? Notes { get; set; }
        public string? AttachmentFileName { get; set; }

     
        public int Status { get; set; }
        public string? StatusMessage { get; set; }
        public int? ApporvalStatus { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy { get; set; }
    }
 
}
