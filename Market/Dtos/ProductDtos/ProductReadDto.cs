using Market.Shared;

namespace Market.Dtos.ProductDtos;

public class ProductReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public EProductStatus Status { get; set; }
}