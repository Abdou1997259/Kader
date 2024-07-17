namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class SelectListOfTransSalaryIncrementResponse
    {
        public int Id { get; set; }
        public int employeeId { get; set; }
        public string employeeName { get; set; }
        public int salrayIncreaseTypeId { get; set; }
        public string salrayIncreaseTypeName { get; set; }
        public string details { get; set; }
        public double increaseValue { get; set; }
        public DateTime transationDate { get; set; }
        public DateTime dueDate { get; set; }

    }
}
