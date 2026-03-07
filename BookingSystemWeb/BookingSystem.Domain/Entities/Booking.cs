using BookingSystem.Domain.Enums;

namespace BookingSystem.Domain.Entities;

public class Booking
{
    public Guid Id { get; private set; }
    public Guid ApartmentId { get; private set; }
    public Apartment Apartment { get; private set; } = null!;
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public DateTime CheckInDate { get; private set; }
    public DateTime CheckOutDate { get; private set; }
    public decimal TotalPrice { get; private set; }
    public BookingStatus BookingStatus { get; private set; }

    internal Booking(Apartment apartment, Guid userId, DateTime start, DateTime end)
    {
        Id = Guid.NewGuid();
        ApartmentId = apartment.Id;
        UserId = userId;
        CheckInDate = start;
        CheckOutDate = end;
        BookingStatus = BookingStatus.Reserved;

        TotalPrice = CalculateTotalPrice(apartment.PricePerNight);
    }

    private Booking()
    {
    }

    private decimal CalculateTotalPrice(decimal pricePerNight)
    {
        var days = (CheckOutDate - CheckInDate).Days;
        if (days <= 0)
        {
            days = 1;
        }

        return days * pricePerNight;
    }

    public void Cancel()
    {
        if (BookingStatus == BookingStatus.Cancelled)
        {
            return;
        }
        if (CheckInDate <= DateTime.UtcNow)
        {
            throw new InvalidOperationException("Cannot cancel a booking that has already started.");
        }
        BookingStatus = BookingStatus.Cancelled;
    }
}