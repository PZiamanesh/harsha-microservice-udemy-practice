namespace OrderMgmt.API.Core.DTOs;

public record ProductResponse
{
    public Guid ProductID { get; init; }
    public string ProductName { get; init; }
    public ProductCategory Category { get; init; }
    public decimal UnitPrice { get; init; }
    public int QuantityInStock { get; init; }
}


public class ProductResponseToOrderItemResponseMapper : Profile
{
    public ProductResponseToOrderItemResponseMapper()
    {
        CreateMap<ProductResponse, OrderItemResponse>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
    }
}