using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreWebAPI.Entities;

public class InventoryItem
{
    [Key]
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }
    public int Quantity { get; set; }


    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;
}
