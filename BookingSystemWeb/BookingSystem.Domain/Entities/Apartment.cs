namespace BookingSystem.Domain.Entities;

public class Apartment
{
    public Guid Id { get; private set; }
    public Guid HostId { get; private set; }
    public User Host { get; private set; } = null!;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public decimal PricePerNight { get; private set; }
    public string? ExternalId { get; set; }
    
    public Apartment(Guid hostId, string title, string description, string address, decimal pricePerNight, string? externalId = null)
    {
        Id = Guid.NewGuid();
        HostId = hostId;
        Title = title;
        Description = description;
        Address = address;
        PricePerNight = pricePerNight;
        ExternalId = externalId;
    }

    private Apartment()
    {
    }

    public Booking Reserve(Guid userId, DateTime start, DateTime end)
    {
        return new Booking(this, userId, start, end);
    }
}