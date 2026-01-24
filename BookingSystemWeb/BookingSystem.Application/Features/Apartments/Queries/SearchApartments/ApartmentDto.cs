namespace BookingSystem.Application.Features.Apartments.Queries.SearchApartments;

public class ApartmentDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal PricePerNight { get; set; }
    public string HostName { get; set; } = string.Empty;
}