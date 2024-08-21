namespace Kader_System.Domain.DTOs.Request.EmployeesRequests
{
    public class GetAllFilltrationForEmployeeRequests : PaginationRequest
    {
        public RequestStatusTypes ApporvalStatus {  get; set; }  
    }
    public class GlobalEmployeeRequests
    {
        public string reson { get; set; }
    }
}
