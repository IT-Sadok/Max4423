namespace BookingSystem.Domain.Entities;

public class Apartment
{
    public Guid Id { get; set; }
    public Guid HostId { get; set; }
    public User Host { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address  { get; set; } = string.Empty;
    public decimal PricePerNight { get; set; }
}