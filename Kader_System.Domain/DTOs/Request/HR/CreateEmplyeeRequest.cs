namespace Kader_System.Domain.DTOs.Request.HR
{
    public class CreateEmployeeRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "first_name_ar")]
        public  string first_name_ar { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "first_name_en")]
        public  string first_name_en { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "father_name_ar")]
        public  string father_name_ar { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "father_name_en")]
        public  string father_name_en { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "grand_father_name_ar")]
        public  string grand_father_name_ar { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "grand_father_name_en")]
        public  string grand_father_name_en { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "family_name_ar")]
        public  string family_name_ar { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "family_name_en")]
        public  string family_name_en { get; set; }
        
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "marital_status_id")]
        public int marital_status_id { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "address")]
        public  string address { get; set; }
        [JsonProperty(PropertyName = "national_id")]
        public string national_id { get; set; }
        [JsonProperty(PropertyName = "job_number")]
        public string job_number { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "hiring_date")]
        public  DateOnly hiring_date { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "immediately_date")]
        public  DateOnly immediately_date { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "is_active")]
        public  bool is_active { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "gender_id")]
        public int gender_id { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "birth_date")]
        public  DateOnly birth_date { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "religion_id")]
        public int religion_id { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "phone")]
        public  string phone { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "email")]
        public  string email { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "salary_payment_way_id")]
        public int salary_payment_way_id { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "username")]
        public  string username { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "password")]
        public  string password { get; set; }
        [DefaultValue(0)]
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "children_number")]
        public  int children_number { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "shift_id")]
        public int shift_id { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "company_id")]
        public int company_id { get; set; }

        /// <summary>
        /// جهاز البصمه
        /// </summary>
        [JsonProperty(PropertyName = "finger_print_id")]
        public int? finger_print_id { get; set; }

        [JsonProperty(PropertyName = "finger_print_code")]
        /// <summary>
        /// كود الموظف المسجل على جهاز البصمه
        /// </summary>
        public string? finger_print_code { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "management_id")]
        public int management_id { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "department_id")]
        public int department_id { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "nationality_id")]
        public int nationality_id { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "qualification_id")]
        public int qualification_id { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "vacation_id")]
        public int vacation_id { get; set; }
        [JsonProperty(PropertyName = "job_id")]
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int job_id { get; set; }

        /// <summary>
        /// مقيم / مواطن
        /// </summary>
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [JsonProperty(PropertyName = "employee_type_id")]
        public int employee_type_id { get; set; }

        [JsonProperty(PropertyName = "account_no")]
        [Display(Name = "account_no")]
        public long? account_no { get; set; }

        [JsonProperty(PropertyName = "note")]
        [Display(Name = "note")]
        public string? note { get; set; }

        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]
        public IFormFile? employee_image { get; set; } = default!;

        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length),
         FileExtensionValidation(FileSettings.AllowedExtension)]
        public IFormFileCollection? employee_attachments { get; set; } = default!;

    }
}
