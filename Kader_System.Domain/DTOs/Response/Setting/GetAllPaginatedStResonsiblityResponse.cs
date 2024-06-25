namespace Kader_System.Domain.DTOs.Response.Setting
{
    public class GetAllPaginatedStResonsiblityResponse : PaginationData<PaginatedStResonsiblity>
    {

    }
    public class PaginatedStResonsiblity
    {
        public int Id { get; set; }
        public string StResponsiblity_Name { get; set; }

        public DateTime? Add_date { get; set; }

    }
}
