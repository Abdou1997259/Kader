namespace Kader_System.Domain.DTOs.Response.HR
{
    public class GetAllEmployeesResponse : PaginationData<EmployeesData>
    {
    }

    public class EmployeesData
    {
        public int Id { get; set; }
       
        public string FullName { get; set; }


        public string MaritalStatus { get; set; }

        public string Address { get; set; }

        public DateOnly HiringDate { get; set; }

        public DateOnly ImmediatelyDate { get; set; }

        public bool IsActive { get; set; }

        public string Gender { get; set; }


        public DateOnly BirthDate { get; set; }

        public string Religion { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string NationalId { get; set; }
        public string JobNumber { get; set; }
        public string SalaryPaymentWay { get; set; }

        public string Username { get; set; }

        public int ChildrenNumber { get; set; }
        public string Shift { get; set; }


        public string Company { get; set; }

        /// <summary>
        /// جهاز البصمه
        /// </summary>
        public string FingerPrint { get; set; }

        /// <summary>
        /// كود الموظف المسجل على جهاز البصمه
        /// </summary>
        public string? FingerPrintCode { get; set; }


        public string Management { get; set; }


        public string Department { get; set; }

        public string Nationality { get; set; }

        public string qualification_name { get; set; }


        public string Vacation { get; set; }

        public string Job { get; set; }

        /// <summary>
        /// مقيم / مواطن
        /// </summary>

        public string EmployeeType { get; set; }


        public string religion_name { get; set; }   
        public string note { get; set; }
        public string nationality_name { get; set; }
        public string marital_status_name { get; set; }
        public int employee_loans_count { get; set; }
        public string management_name { get; set; }
        public string job_name { get; set; }
        public string company_name { get; set; }
        public string department_name { get; set; }
        public int vacation_days_count { get; set; }
        public bool IsDeleted { get; set; }
    }
}
