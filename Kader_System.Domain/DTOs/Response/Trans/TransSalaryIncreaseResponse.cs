using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class GetAllSalaryIncreaseResponse : PaginationData<TransSalaryIncreaseResponse>
    {
    }


    public class TransSalaryIncreaseResponse
    {
        public int Id { get; set; } 
        public int EmployeeId {  get; set; }    
        public string EmployeeName { get; set; }=string.Empty;
        public double PreviousSalary { get; set; }
        public double AfterIncreaseSalary { get; set; }
        public string AddedBy { get; set; } = string.Empty;

        public string SalaryIncreaseType { get; set; } = string.Empty;

        public double IncreaseValue { get; set; }
        public DateOnly TransactionDate { get; set; }


    }
}
