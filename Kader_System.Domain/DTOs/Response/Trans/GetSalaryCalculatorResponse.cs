namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class GetSalaryCalculatorResponse
    {
        public int Id { get; set; }

        public DateTime? DocDate { get; set; }
        public DateOnly CalculationDate { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }


    }
}
