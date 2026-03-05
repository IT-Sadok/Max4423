using BookingSystem.Api.Models.Analytics;
using BookingSystem.Application.Features.Analytics.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;

[Route("api/analytics")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AnalyticsController:ControllerBase
{
    private readonly IMediator _mediator;

    public AnalyticsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("top-host-performance")]
    public async Task<IActionResult> GetTopHostPerformance()
    {
        var result = await _mediator.Send(new GetTopHostPerformanceQuery());
        return Ok(result);
    }

    [HttpGet("monthly-dynamics")]
    public async Task<IActionResult> GetMonthlyDynamics()
    {
        var result = await _mediator.Send(new GetMonthlyDynamicsQuery());
        return Ok(result);
    }

    [HttpGet("market-price-percentiles")]
    public async Task<IActionResult> GetMarketPricePercentiles()
    {
        var result = await _mediator.Send(new GetMarketPricePercentilesQuery());
        return Ok(result);
    }
    
    [HttpGet("apartment-occupancy")]
    public async Task<IActionResult> GetApartmentOccupancy(
        [FromQuery] GetApartmentOccupancyRequest request) 
    {
        var result = await _mediator.Send(new GetApartmentOccupancyQuery(request.StartDate, 
            request.EndDate, request.PageNumber, request.PageSize));
        return Ok(result);
    }
    
    [HttpGet("host-portfolio")]
    public async Task<IActionResult> GetHostPortfolio([FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return BadRequest("PageNumber and PageSize must be greater than zero.");
        }
        
        var result = await _mediator.Send(new GetHostPortfolioQuery(pageNumber, pageSize));
        return Ok(result);
    }
}