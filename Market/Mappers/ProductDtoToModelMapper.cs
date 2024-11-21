using Market.Dtos.ProductDtos;

namespace Market.Mappers;
public static class ProductDtoToModelMapper
{
    public static Models.Product ToModel(this ProductCreateDto dto)
    {
        return new Models.Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        };
    }

    public static ProductReadDto ToReadDto(this Models.Product model)
    {
        return new ProductReadDto
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            CreatedAt = model.CreatedAt,
            ModifiedAt = model.ModifiedAt,
            Status = model.Status
        };
    }

    public static void UpdateModel(this Models.Product model, ProductUpdateDto dto)
    {
        model.Name = dto.Name;
        model.Price = dto.Price;
        model.Status = dto.Status;
        model.ModifiedAt = DateTime.UtcNow;
    }
}

