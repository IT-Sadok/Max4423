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
}