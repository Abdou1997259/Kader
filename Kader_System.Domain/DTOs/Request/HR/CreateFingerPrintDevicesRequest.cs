namespace Kader_System.Domain.DTOs.Request.HR
{
    public class CreateFingerPrintDeviceRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string IPAddress { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public  string NameAr { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public  string NameEn { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public  string Password { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public  string Port { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public  string Username { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public  int CompanyId { get; set; }
    }
}
