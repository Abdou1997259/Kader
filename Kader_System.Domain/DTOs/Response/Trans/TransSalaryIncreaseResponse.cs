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
        public string employeeName { get; set; }
        public double PreviousSalary { get; set; }
        public double AfterIncreaseSalary { get; set; }
        public string AddedBy { get; set; }

        public string salrayIncreaseType { get; set; }

        public double increaseValue { get; set; }
        public DateTime transationDate { get; set; }


    }
}
