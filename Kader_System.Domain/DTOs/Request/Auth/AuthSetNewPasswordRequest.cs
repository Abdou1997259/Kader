namespace Kader_System.Domain.Dtos.Request.Auth;

public class AuthSetNewPasswordRequest
{
    public required string OldPassword { get; set; } 
    public required string NewPassword { get; set; } 
    public required string ConfirmPassword { get; set; } 
}
