namespace Kader_System.Domain.Dtos.Request.Setting;

public class StUpdateSubScreenRequest : StCreateSubScreenRequest
{
    [Required(ErrorMessage = Annotations.FieldIsRequired)]
    public string screen_code { get; set; }
}
