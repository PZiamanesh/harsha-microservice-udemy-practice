namespace OrderMgmt.API.Core.DTOs;

public record OrderResponse
{
    public required Guid OrderID { get; set; }
    public required Guid UserID { get; set; }
    public required DateTime OrderDate { get; set; }
    public required decimal TotalBill { get; set; }
    public required string UserPersonName { get; set; }
    public required string Email { get; set; }
    public List<OrderItemResponse> OrderItems { get; set; } = [];
}


public class OrderResponseMapper : Profile
{
    public OrderResponseMapper()
    {
        CreateMap<Order, OrderResponse>()
            .ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.OrderID))
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
            .ForMember(dest => dest.TotalBill, opt => opt.MapFrom(src => src.TotalBill))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
    }
}