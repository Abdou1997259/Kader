namespace Kader_System.Domain.DTOs.Request.Auth
{
    public class UpdateUserRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string user_name { get; set; }

        public string? password { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string phone { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string email { get; set; }

        public string full_name { get; set; }

        public int? current_title { get; set; }

        public int? current_company { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public List<int?> title_id { get; set; } = new List<int?>() { };
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public List<int?> company_id { get; set; } = new List<int?>() { };
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int job_title { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int financial_year { get; set; }
        public IFormFile? image { get; set; }

        public bool is_active { get; set; }
    }
}
