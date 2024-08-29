using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Models.HR
{
    public class SPEmployeeDetails
    {    
        public int Id { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public int ChildrenNumber { get; set; }
        public string Email { get; set; }
        public string EmployeeType { get; set; }
        public string FingerPrintCode { get; set; }
        public string FullName { get; set; }
        public DateTime? HiringDate { get; set; }
        public DateTime? ImmediatelyDate { get; set; }
        public bool IsActive { get; set; }
        public string JobNumber { get; set; }
        public string NationalId { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string Job { get; set; }
        public string QualificationName { get; set; }
        public int TitleId { get; set; }
        public string Phone { get; set; }
        public int VacationDaysCount { get; set; }
        public string Username { get; set; }
        public string Vacation { get; set; }
        public string DepartmentName { get; set; }
        public string ManagementName { get; set; }
        public string MaritalStatusName { get; set; }
        public string CompanyName { get; set; }
        public string Shift { get; set; }
        public string SalaryPaymentWay { get; set; }
        public string Gender { get; set; }
    }
}
