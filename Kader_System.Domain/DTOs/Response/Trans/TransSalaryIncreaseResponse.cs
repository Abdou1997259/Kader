using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class TransSalaryIncreaseResponse
    {
        public int employeeId {  get; set; }
        public int salrayIncreaseTypeId {  get; set; }
        public string details { get; set; }
        public double increaseValue { get; set; }
    }
}
