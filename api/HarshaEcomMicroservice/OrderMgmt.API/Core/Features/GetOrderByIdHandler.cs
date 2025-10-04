
namespace OrderMgmt.API.Core.Features;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdRequest, ErrorOr<OrderResponse>>
{
    private readonly IOrderRepository  _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdHandler(
        IOrderRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<OrderResponse>> Handle(
        GetOrderByIdRequest request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsycn(request.OrderID);

        throw new NotImplementedException();
    }
}
