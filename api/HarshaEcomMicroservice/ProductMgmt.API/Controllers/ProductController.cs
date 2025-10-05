namespace ProductMgmt.API.Controllers;

[Route("api/products")]
public class ProductController : ApiController
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsFilter filter)
    {
        var response = await _mediator.Send(filter);

        return Ok(response.Value);
    }

    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        var request = new GetProductByIdRequest { ProductID = productId };
        var response = await _mediator.Send(request);

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return Ok(response.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request)
    {
        var response = await _mediator.Send(request);

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }
}
