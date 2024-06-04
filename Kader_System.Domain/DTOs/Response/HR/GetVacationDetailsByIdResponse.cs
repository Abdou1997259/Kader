using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.HR
{
    public class GetVacationDetailsByIdResponse
    {
        public int ApplyAfterMonth { get; set; }
        public int TotalBalance { get; set; }
        public bool CanTransfer { get; set; }
        public  string NameAr { get; set; }
        public string NameEn { get; set; }
        public string VacationType { get; set; }
        public int VacationTypeId { get; set; }

        public List<VacationDistributionResponseData> Details { get; set; }=new List<VacationDistributionResponseData>();
    }

    public class VacationDistributionResponseData
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int DaysCount { get; set; }
        public int SalaryCalculatorId { get; set; }
    }
}
