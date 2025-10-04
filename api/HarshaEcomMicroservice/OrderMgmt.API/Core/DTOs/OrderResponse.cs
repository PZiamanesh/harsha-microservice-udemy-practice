namespace OrderMgmt.API.Core.DTOs;

public record OrderResponse
{
    public Guid OrderID { get; set; }
    public Guid UserID { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalBill { get; set; }
    public string UserPersonName { get; set; }
    public string Email { get; set; }
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