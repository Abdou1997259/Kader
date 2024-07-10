namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CreateTransSalaryIncreaseRequest
    {
        public int employeeId { get; set; }
        public int salrayIncreaseTypeId { get; set; }
        public string details { get; set; }
        public double increaseValue { get; set; }
    }
}
