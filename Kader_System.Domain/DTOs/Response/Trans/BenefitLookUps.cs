using Kader_System.Domain.DTOs.Response.Loan;

namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class BenefitLookUps
    {
        public List<EmployeeLookup> employees { get; set; }
        public object[] benefit { get; set; }
        public object[] salary_effects { get; set; }
        public object[] trans_amount_types { get; set; }
    }
}
