namespace UserMgmt.API.Controllers;

[Route("api/users")]
public class UserController : ApiController
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var request = new GetUserByIdRequest { UserID = userId };
        var response = await mediator.Send(request);

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return Ok(response.Value);
    }
}
