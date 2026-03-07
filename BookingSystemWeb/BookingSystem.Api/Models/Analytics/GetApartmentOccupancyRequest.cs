namespace BookingSystem.Api.Models.Analytics;

public record GetApartmentOccupancyRequest()
{
    public DateTime StartDate { get; init; } = DateTime.UtcNow.AddYears(-1);
    public DateTime EndDate { get; init; } = DateTime.UtcNow;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}