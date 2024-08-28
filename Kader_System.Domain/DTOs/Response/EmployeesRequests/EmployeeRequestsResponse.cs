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
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string? request_date { get; set; }
        public string? Notes { get; set; }
        public int? ApporvalStatus { get; set; }
        public string reason { get; set; }
        public string? AttachmentPath { get; set; }               
    }
    public class ListOfLeavePermissionsRequestResponse : EmployeeRequestsResponse
    {
        public TimeOnly LeaveTime { get; set; }
        public TimeOnly? BackTime { get; set; }
    }
    public class ListOfDelayPermissionRequestResponse : EmployeeRequestsResponse
    {
        public int? HoursDelay { get; set; }
        public TimeOnly? ArrivalTime { get; set; }
    }  
    public class ListOfAllowanceRequestResponse : EmployeeRequestsResponse
    {
        public int? allowance_id { get;set; }
        public int? allowance_type_id { get;set; }
        public string? allowance_name { get; set; }
        public string? allowance_type_name { get; set; }
        public double amount { get; set; }
    }
    public class ListOfContractTerminationRequestResponse : EmployeeRequestsResponse
    {

    }  
    public class ListOfResignationRequestResponse : EmployeeRequestsResponse
    {

    } 
    public class ListOfLoanRequestResponse : EmployeeRequestsResponse
    {
        public int InstallmentsCount { get; set; }
        public DateOnly StartDate { get; set; }
        public decimal Amount { get; set; }
    }
    public class ListOfSalaryIncreaseRequestResponse : EmployeeRequestsResponse
    {
        public double Amount { get; set; }
    } 
    public class ListOfVacationRequestResponse : EmployeeRequestsResponse
    {
        public int DayCounts { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int VacationTypeId { get; set; }
        public string VacationTypeName { get; set; }
    }
}
