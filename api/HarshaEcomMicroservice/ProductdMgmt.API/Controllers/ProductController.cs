namespace ProductdMgmt.API.Controllers;

[Route("api/products")]
public class ProductController : ApiController
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var result = await _mediator.Send(new GetProductsRequest());

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductRequest request)
    {
        var result = await _mediator.Send(request);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return NoContent();
    }
}
