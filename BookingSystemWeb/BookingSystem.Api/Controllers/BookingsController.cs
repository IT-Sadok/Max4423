using MediatR;
using Microsoft.AspNetCore.Mvc;
using BookingSystem.Application.Features.Bookings.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BookingSystem.Api.Controllers;

[Route("api/bookings")]
[ApiController]
public class BookingsController: ControllerBase
{
    private readonly IMediator _mediator;

    public BookingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]    
    public async Task<IActionResult> Create(CreateBookingCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(new { Error = result.ErrorMessage });
    }
}