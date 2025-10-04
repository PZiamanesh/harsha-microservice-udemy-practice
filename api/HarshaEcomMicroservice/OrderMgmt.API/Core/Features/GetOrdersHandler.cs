
using OrderMgmt.API.Infrastructure.HttpClientServices;

namespace OrderMgmt.API.Core.Features;

public class GetOrdersHandler : IRequestHandler<GetOrdersFilter, ErrorOr<IEnumerable<OrderResponse>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ProductClientService _productClientService;
    private readonly UserClientService _userClientService;

    public GetOrdersHandler(
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

    public async Task<ErrorOr<IEnumerable<OrderResponse>>> Handle(
        GetOrdersFilter filter,
        CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersAsync(filter);

        if (!orders.Any())
        {
            return new List<OrderResponse>();
        }

        var orderResponses = _mapper.Map<List<OrderResponse>>(orders);

        foreach (var orderResponse in orderResponses)
        {
            var userResponse = await _userClientService.GetUserById(orderResponse.UserID);

            if (userResponse != null)
            {
                _mapper.Map<UserResponse, OrderResponse>(userResponse, orderResponse);
            }

            foreach (var orderItemResponse in orderResponse.OrderItems)
            {
                var productResponse = await _productClientService.GetProductById(orderItemResponse.ProductID);

                if (productResponse != null)
                {
                    _mapper.Map<ProductResponse, OrderItemResponse>(productResponse, orderItemResponse);
                }
            }
        }

        return orderResponses;
    }
}
