using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserMgmt.API.Core.Dto;

namespace UserMgmt.API.Controllers;

[Route("api/auth")]
public class AuthenticationController : ApiController
{
    private readonly IMediator mediator;

    public AuthenticationController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("registerUser")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        var response = await mediator.Send(request);

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return Ok(response.Value);
    }

    [HttpPost("loginUser")]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
    {
        var response = await mediator.Send(request);

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return Ok(response.Value);
    }
}
