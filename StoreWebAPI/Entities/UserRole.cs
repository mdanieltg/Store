using System.ComponentModel.DataAnnotations;

namespace Store.WebAPI.Entities;

public class UserRole
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(40)]
    public required string Name { get; set; }


    public HashSet<User> Users { get; set; } = new();
}
