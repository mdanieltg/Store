using System.Diagnostics;

namespace Store.WebAPI.DataTransferObjects;

[DebuggerDisplay("Username: {Username}")]
public class UserDto
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required byte[] Password { get; set; }
    public required byte[] Salt { get; set; }
    public bool IsLocked { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset LastLoginDate { get; set; }
}
