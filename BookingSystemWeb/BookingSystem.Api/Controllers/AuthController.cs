using BookingSystem.Application.Features.Auth.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController:ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var userId = await _mediator.Send(command);
        
        return Ok(userId);
    }

}