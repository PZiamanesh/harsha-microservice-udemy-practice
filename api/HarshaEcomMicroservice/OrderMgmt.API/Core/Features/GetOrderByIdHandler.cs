
using OrderMgmt.API.Infrastructure.HttpClientServices;

namespace OrderMgmt.API.Core.Features;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdRequest, ErrorOr<OrderResponse>>
{
    private readonly IOrderRepository  _orderRepository;
    private readonly IMapper _mapper;
    private readonly ProductClientService _productClientService;
    private readonly UserClientService _userClientService;

    public GetOrderByIdHandler(
        IOrderRepository orderRepository,
        IMapper mapper,
        ProductClientService productClientService,
        UserClientService userClientService)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _productClientService = productClientService;
        _userClientService = userClientService;
    }

    public async Task<ErrorOr<OrderResponse>> Handle(
        GetOrderByIdRequest request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsycn(request.OrderID);

        if (order is null)
        {
            return Error.NotFound($"Order with id: {request.OrderID} was not found");
        }

        var orderResponse = _mapper.Map<OrderResponse>(order);

        foreach (var orderItemResponse in orderResponse.OrderItems)
        {
            var productResponse = await _productClientService.GetProductById(orderItemResponse.ProductID);

            if (productResponse is not null)
            {
                _mapper.Map<ProductResponse, OrderItemResponse>(productResponse, orderItemResponse);
            }
        }

        var userResponse = await _userClientService.GetUserById(orderResponse.UserID);

        if (userResponse != null)
        {
            _mapper.Map<UserResponse, OrderResponse>(userResponse, orderResponse);
        }

        return orderResponse;
    }
}
