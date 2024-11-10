namespace StoreWebAPI.DataTransferObjects;

public class ProductDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
