using BookingSystem.Domain.Common;
using MediatR;

namespace BookingSystem.Application.Features.Bookings.Commands.CancelBooking;

public class CancelBookingCommand: IRequest<Result<bool>>
{
    public Guid BookingId { get; init; }
}