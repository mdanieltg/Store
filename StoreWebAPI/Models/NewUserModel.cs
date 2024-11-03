using System.ComponentModel.DataAnnotations;
using StoreWebAPI.ValidationAttributes;

namespace StoreWebAPI.Models;

public class NewUserModel
{
    [StringLength(50, MinimumLength = 1)]
    public required string FirstName { get; set; }

    [StringLength(50, MinimumLength = 1)]
    public string? MiddleName { get; set; }

    [StringLength(50, MinimumLength = 1)]
    public required string LastName { get; set; }

    [StringLength(20, MinimumLength = 1)]
    public required string Username { get; set; }

    [EmailAddress]
    [StringLength(255)]
    public required string Email { get; set; }

    [Password]
    public required string Password { get; set; }
    public required string Role { get; set; }
}
