using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreWebAPI.Entities;

public class Order
{
    [Key]
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Vat { get; set; }
    public decimal Total { get; set; }


    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    public HashSet<OrderItem> OrderItems { get; set; } = new();
}
