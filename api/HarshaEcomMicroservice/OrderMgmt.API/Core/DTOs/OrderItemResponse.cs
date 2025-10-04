namespace OrderMgmt.API.Core.DTOs;

public record OrderItemResponse
{
    public Guid ProductID { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string ProductName { get; set; }
    public ProductCategory Category { get; set; }
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