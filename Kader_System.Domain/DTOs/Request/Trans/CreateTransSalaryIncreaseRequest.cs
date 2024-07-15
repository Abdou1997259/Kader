namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CreateTransSalaryIncreaseRequest
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string? Notes { get; set; }
        public int Increase_type { get; set; }
        public int Employee_id { get; set; }
    }
}
