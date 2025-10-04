namespace OrderMgmt.API.Core.DTOs;

public record OrderItemResponse
{
    public required Guid ProductID { get; set; }
    public required decimal UnitPrice { get; set; }
    public required int Quantity { get; set; }
    public required decimal TotalPrice { get; set; }
    public required string ProductName { get; set; }
    public required ProductCategory Category { get; set; }
}


public class OrderItemResponseMapper : Profile
{
    public OrderItemResponseMapper()
    {
        CreateMap<OrderItem, OrderItemResponse>()
            .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));
    }
}