using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.WebAPI.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }

    public Guid RoleId { get; set; }

    [MaxLength(20)]
    public required string Username { get; set; }

    [MaxLength(255)]
    public required string Email { get; set; }

    [MaxLength(32)]
    public required byte[] Password { get; set; }

    [MaxLength(32)]
    public required byte[] Salt { get; set; }

    public bool IsLocked { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset LastLoginDate { get; set; }


    [ForeignKey(nameof(RoleId))]
    public UserRole Role { get; set; } = null!;

    public Customer Customer { get; set; } = null!;
}
