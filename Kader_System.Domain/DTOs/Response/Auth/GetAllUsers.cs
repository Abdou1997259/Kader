
namespace Kader_System.Domain.DTOs.Response.Auth
{
    public class GetAllUsersResponse:PaginationData<ListOfUsersResponse>
    {

    }
    public class ListOfUsersResponse
    {
        public string Id { get; set; }  
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public int FinancialYear { get; set; }    
        public string Email { get; set; }   
        public string Phone { get; set; }
        public string JobName { get; set; } 

       

    }
}
