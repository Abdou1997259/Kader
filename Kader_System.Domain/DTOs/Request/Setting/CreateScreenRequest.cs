namespace Kader_System.Domain.DTOs.Request.Setting
{
    public class CreateScreenRequest
    {
        public int? ParentId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string NameAr { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string NameEn { get; set; }
        [AllowedValues(1, 2, 3, ErrorMessage = "Invalid ScreenType value. Allowed values are 1, 2, or 3.")]
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int ScreenType { get; set; }
        public string? Url { get; set; }
        public string? EndPoint { get; set; }
        public IFormFile? Icon { get; set; }
        public IFormFile? ActiveIcon { get; set; }
        public int Sort { get; set; }
        public List<int> Actions { get; set; } = [];
    }
}
