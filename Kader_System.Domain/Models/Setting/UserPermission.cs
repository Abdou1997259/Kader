

namespace Kader_System.Domain.Models.Setting
{
    [Table("st_user_permissions")]
    public class UserPermission
    {
        [Key]
        public int Id { get; set; } 
        public string UserId { get; set; }
        public int SubScreenId {  get; set; }   
        public string Permission { get; set; }
        public int TitleId { get; set; }
 
               
    }
}
