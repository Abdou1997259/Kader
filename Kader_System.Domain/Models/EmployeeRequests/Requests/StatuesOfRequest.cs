
using Microsoft.EntityFrameworkCore;

namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Owned]
    public class StatuesOfRequest
    {
        [Column("Status")]
        public RequestStatusTypes? StatusTypes { get; set; }
        [Column("StatusMessage")]
        public string? StatusMessage { get; set; }
        [Column("ApporvalStatus")]
        public int? ApporvalStatus { get; set; }
        [Column("ApprovedDate")]
        public DateTime? ApprovedDate { get; set; }
        [Column("ApprovedBy")]
        public int? ApprovedBy { get; set; }
    }
}
