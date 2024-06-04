namespace Kader_System.Domain.DTOs.Response.HR
{
    public class ListOfEmployeesResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
      
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string Company { get; set; }
        public string Management { get; set; }
        public string Job { get; set; }


        public string Address { get; set; }

        public DateOnly HiringDate { get; set; }

        public DateOnly ImmediatelyDate { get; set; }

        public bool IsActive { get; set; }
    }
}
