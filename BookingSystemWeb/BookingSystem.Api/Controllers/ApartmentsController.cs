using BookingSystem.Application.Features.Apartments.Commands.UpsertApartment;
using BookingSystem.Application.Features.Apartments.Queries.SearchApartments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;

[Route("api/apartments")]
[ApiController]
public class ApartmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Search([FromQuery] DateTime? checkInDate, [FromQuery] DateTime? checkOutDate,
        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new SearchApartmentsQuery(checkInDate, checkOutDate, pageNumber, pageSize);
        
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.ErrorMessage);
    }
    
    [HttpPost("upsert")]
    public async Task<IActionResult> Upsert([FromBody] UpsertApartmentCommand command)
    {
        var success = await _mediator.Send(command);
        
        if (success)
        {
            return Ok(new { Message = "Apartment upserted successfully." });
        }
        
        return BadRequest("Failed to upsert apartment.");
    }
}