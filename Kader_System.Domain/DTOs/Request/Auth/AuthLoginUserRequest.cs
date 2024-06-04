namespace Kader_System.Domain.Dtos.Request.Auth;

public class AuthLoginUserRequest
{
    [Display(Name = Annotations.UserName), Required(ErrorMessage = Annotations.FieldIsRequired)]
    [DefaultValue("admin")]
    public required string UserName { get; set; } 

    [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
    [Display(Name = Annotations.Password), Required(ErrorMessage = Annotations.FieldIsRequired)]
    [DefaultValue("123456")]
    public required string Password { get; set; } 

    [Display(Name = Annotations.RememberMe)]
    public bool RememberMe { get; set; }

    //public string? DeviceId { get; set; }
}
