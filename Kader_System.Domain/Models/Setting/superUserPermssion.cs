namespace Kader_System.Domain.Models.Setting
{
    public class SuperUserPermssion
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SubScreenId { get; set; }
        public string Permission { get; set; }
        public int TitleId { get; set; }
    }
}
