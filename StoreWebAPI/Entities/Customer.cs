using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.WebAPI.Entities;

public class Customer
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    [MaxLength(50)]
    public required string FirstName { get; set; }

    [MaxLength(50)]
    public string? MiddleName { get; set; }

    [MaxLength(50)]
    public required string LastName { get; set; }


    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    public HashSet<Order> Orders { get; set; } = new();
}
