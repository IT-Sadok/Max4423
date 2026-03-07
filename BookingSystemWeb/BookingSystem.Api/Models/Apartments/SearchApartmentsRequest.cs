namespace BookingSystem.Api.Models.Apartments;

public record SearchApartmentsRequest()
{
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

}