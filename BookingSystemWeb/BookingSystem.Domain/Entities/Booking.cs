using BookingSystem.Domain.Enums;

namespace BookingSystem.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; }
    public Guid ApartmentId { get; set; }
    public Apartment Apartment { get; private set; } = null!;
    public Guid UserId { get; set; }
    public User User { get; private set; } = null!;
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus BookingStatus { get; set; }

    public static Booking Reserve(Apartment apartment, Guid userId, DateTime start, DateTime end)
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            ApartmentId = apartment.Id,
            UserId = userId,
            CheckInDate = start,
            CheckOutDate = end,
            BookingStatus = BookingStatus.Reserved
        };

        booking.TotalPrice = booking.CalculateTotalPrice(apartment.PricePerNight);

        return booking;
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
}