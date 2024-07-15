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
        public int employeeId {  get; set; }
        public string employeeName {  get; set; }
        public int salrayIncreaseTypeId {  get; set; }
        public string salrayIncreaseTypeName {  get; set; }
        public string details { get; set; }
        public double increaseValue { get; set; }
        public DateTime transationDate { get; set; }
        public DateTime dueDate { get; set; }

    }
}
