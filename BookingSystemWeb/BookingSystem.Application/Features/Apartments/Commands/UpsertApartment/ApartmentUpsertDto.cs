namespace BookingSystem.Application.Common.DTOs;

public class ApartmentUpsertDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Address { get; set; } = null!;
    public decimal PricePerNight { get; set; }
    public string ExternalId { get; set; } = null!;
    public Guid HostId { get; set; }
    public string? CustomData { get; set; }
}