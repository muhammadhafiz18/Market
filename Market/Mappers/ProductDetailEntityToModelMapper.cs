namespace Market.Mappers;
public static class ProductDetailEntityToModelMapper
{
    public static Models.ProductDetail ToModel(this Entities.ProductDetail entity)
    {
        return new Models.ProductDetail
        {
            Description = entity.Description,
            Color = entity.Color,
            Material = entity.Material,
            Weight = entity.Weight,
            QuantityInStock = entity.QuantityInStock,
            ManufactureDate = entity.ManufactureDate,
            ExpiryDate = entity.ExpiryDate,
            Size = entity.Size,
            Manufacturer = entity.Manufacturer,
            CountryOfOrigin = entity.CountryOfOrigin
        };
    }

    public static Entities.ProductDetail ToEntity(this Models.ProductDetail model)
    {
        return new Entities.ProductDetail
        {
            Description = model.Description,
            Color = model.Color,
            Material = model.Material,
            Weight = model.Weight,
            QuantityInStock = model.QuantityInStock,
            ManufactureDate = model.ManufactureDate,
            ExpiryDate = model.ExpiryDate,
            Size = model.Size,
            Manufacturer = model.Manufacturer,
            CountryOfOrigin = model.CountryOfOrigin
        };
    }
}

