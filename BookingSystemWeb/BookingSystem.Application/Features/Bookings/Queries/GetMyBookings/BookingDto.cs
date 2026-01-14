using BookingSystem.Domain.Enums;

namespace BookingSystem.Application.Features.Bookings.Queries.GetMyBookings;

public class BookingDto
{
    public Guid Id { get; set; }
    public Guid ApartmentId { get; set; }
    public string ApartmentTitle { get; set; } = string.Empty;
    public string ApartmentDescription { get; set; } = string.Empty;
    public string ApartmentAddress { get; set; } = string.Empty;
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
}