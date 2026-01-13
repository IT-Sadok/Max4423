using BookingSystem.Domain.Common;
using MediatR;

namespace BookingSystem.Application.Features.Bookings.Commands;

public class CreateBookingCommand : IRequest<Result<Guid>>
{
    public Guid ApartmentId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
}