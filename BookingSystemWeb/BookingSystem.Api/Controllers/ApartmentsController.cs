using BookingSystem.Application.Features.Apartments.Queries.SearchApartments;
using BookingSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;

[Route("api/[controller]")]
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
    public async Task<IActionResult> Search([FromQuery] DateTime? checkInDate, [FromQuery] DateTime? checkOutDate)
    {
        var query = new SearchApartmentsQuery
        {
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate
        };
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.ErrorMessage);
    }
}