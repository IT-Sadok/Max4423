using System.Security.Claims;
using BookingSystem.Api.Models.Apartments;
using BookingSystem.Application.Features.Apartments.Commands.UpsertApartment;
using BookingSystem.Application.Features.Apartments.Queries.SearchApartments;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public async Task<IActionResult> Search([FromQuery] SearchApartmentsRequest request)
    {
        var query = new SearchApartmentsQuery(request.CheckInDate, request.CheckOutDate, request.PageNumber, request.PageSize);
        
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.ErrorMessage);
    }
    
    [HttpPost("upsert")]
    [Authorize(Roles = "Host", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Upsert([FromBody] UpsertApartmentCommand command)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var currentUserId))
        {
            return Unauthorized("User ID is missing or invalid in token.");
        }
        
        var secureCommand = command with { HostId = currentUserId };
        
        var apartmentId = await _mediator.Send(secureCommand);
        
        if (apartmentId != Guid.Empty)
        {
            return Ok(new 
            { 
                Message = "Apartment upserted successfully.", 
                Id = apartmentId 
            });
        }
        
        return BadRequest("Failed to upsert apartment. It might belong to another user.");
    }
}