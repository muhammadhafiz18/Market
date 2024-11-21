namespace Market.Mappers;
public static class ProductEntityToModelMapper
{
    public static Models.Product ToModel(this Entities.Product entity)
    {
        return new Models.Product
        {
            Id = entity.Id,
            Name = entity.Name,
            Price = entity.Price,
            CreatedAt = entity.CreatedAt,
            ModifiedAt = entity.ModifiedAt,
            Status = entity.Status,
            ProductDetail = entity.ProductDetail?.ToModel()
        };
    }

    public static Entities.Product ToEntity(this Models.Product model)
    {
        return new Entities.Product
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            CreatedAt = model.CreatedAt,
            ModifiedAt = model.ModifiedAt,
            Status = model.Status,
            ProductDetail = model.ProductDetail?.ToEntity()
        };
    }
}

