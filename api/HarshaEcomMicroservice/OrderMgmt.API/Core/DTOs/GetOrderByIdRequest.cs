namespace OrderMgmt.API.Core.DTOs;

public struct GetOrderByIdRequest : IRequest<ErrorOr<OrderResponse>>
{
    public Guid OrderID { get; set; }
}
