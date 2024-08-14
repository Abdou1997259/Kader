namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class SelectListOfTransSalaryIncrementResponse
    {
        public int Id { get; set; }
     
        public string employeeName { get; set; }
        public double PreviousSalary { get; set; }
        public double AfterIncreaseSalary { get; set; }
        public string AddedBy { get; set; }       
  
        public string salrayIncreaseType { get; set; }
     
        public double increaseValue { get; set; }
        public DateOnly transationDate { get; set; }
   

    }
}
