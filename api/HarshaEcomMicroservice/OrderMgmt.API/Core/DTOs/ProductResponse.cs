namespace OrderMgmt.API.Core.DTOs;

public record ProductResponse
{
    public Guid ProductID { get; set; }
    public string? ProductName { get; set; }
    public CategoryOptions Category { get; set; }
    public double UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
}


public class ProductResponseMapper : Profile
{
    public ProductResponseMapper()
    {
        // Product -> ProductResponse
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));
    }
}