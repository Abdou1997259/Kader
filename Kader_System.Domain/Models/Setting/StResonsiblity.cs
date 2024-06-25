

namespace Kader_System.Domain.Models.Setting
{
    [Table("St_Resonsiblities")]
    public class StResonsiblity : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string StResponsiblity_Name_Ar { get; set; }
        public string StResponsiblity_Name_En { get; set; }

    }
}
