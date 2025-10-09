namespace ProductMgmt.API.Core.DTOs;

public record UpdateProductMessage
{
    public Guid ProductID { get; set; }
    public string ProductName { get; set; }
    public ProductCategory Category { get; set; }
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
}


public class UpdateProductMessageMapper : Profile
{
    public UpdateProductMessageMapper()
    {
        CreateMap<Product, UpdateProductMessage>()
            .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));
    }
}
