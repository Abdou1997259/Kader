namespace Kader_System.Domain.Models.Auth;

[Table("Auth_UserDevices")]
public class ApplicationUserDevice : BaseEntity
{
    [Key]
    public int Id { get; set; }

    public string DeviceId { get; set; } = string.Empty;


    [ForeignKey(nameof(UserId))]
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = default!;
}
