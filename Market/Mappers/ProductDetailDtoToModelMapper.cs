using Market.Dtos.ProductDetailDtos;

namespace Market.Mappers;
public static class ProductDetailDtoToModelMapper
{
    public static Models.ProductDetail ToModel(this ProductDetailCreateDto dto)
    {
        return new Models.ProductDetail
        {
            Description = dto.Description,
            Color = dto.Color,
            Material = dto.Material,
            Weight = dto.Weight,
            QuantityInStock = dto.QuantityInStock,
            ManufactureDate = dto.ManufactureDate,
            ExpiryDate = dto.ExpiryDate,
            Size = dto.Size,
            Manufacturer = dto.Manufacturer,
            CountryOfOrigin = dto.CountryOfOrigin
        };
    }

    public static Models.ProductDetail ToUpdateModel(this ProductDetailUpdateDto dto)
    {
        return new Models.ProductDetail
        {
            Description = dto.Description,
            Color = dto.Color,
            Material = dto.Material,
            Weight = dto.Weight,
            QuantityInStock = dto.QuantityInStock,
            ManufactureDate = dto.ManufactureDate,
            ExpiryDate = dto.ExpiryDate,
            Size = dto.Size,
            Manufacturer = dto.Manufacturer,
            CountryOfOrigin = dto.CountryOfOrigin
        };
    }

    public static ProductDetailReadDto ToReadDto(this Models.ProductDetail model)
    {
        return new ProductDetailReadDto
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

    public static void UpdateModel(this Models.ProductDetail model, ProductDetailUpdateDto dto)
    {
        model.Description = dto.Description;
        model.Color = dto.Color;
        model.Material = dto.Material;
        model.Weight = dto.Weight;
        model.QuantityInStock = dto.QuantityInStock;
        model.ManufactureDate = dto.ManufactureDate;
        model.ExpiryDate = dto.ExpiryDate;
        model.Size = dto.Size;
        model.Manufacturer = dto.Manufacturer;
        model.CountryOfOrigin = dto.CountryOfOrigin;
    }
}

