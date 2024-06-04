namespace Kader_System.Domain.Models.HR
{
    [Table("Hr_Managements")]
    public class HrManagement:BaseEntity
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int? ManagerId { get; set; }
        [ForeignKey(nameof(ManagerId))]
        public HrEmployee Manager { get; set; } 
        public int CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public HrCompany Company { get; set;}
    }
}
