namespace OrderMgmt.API.Controllers;

[Route("api/orders")]
public class OrderController : ApiController
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetOrderById(Guid orderId)
    {
        var request = new GetOrderByIdRequest { OrderID = orderId };
        var response = await _mediator.Send(request);

        return Ok(response.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders([FromQuery] GetOrdersFilter filter)
    {
        var response = await _mediator.Send(filter);

        return Ok(response.Value);
    }
}
