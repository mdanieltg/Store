using System.ComponentModel.DataAnnotations;

namespace Store.WebAPI.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(255)]
    public required string Name { get; set; }


    public InventoryItem InventoryItem { get; set; } = null!;
    public HashSet<OrderItem> OrderItems { get; set; } = new();
}
