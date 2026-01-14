using BookingSystem.Domain.Common;
using MediatR;

namespace BookingSystem.Application.Features.Bookings.Queries.GetMyBookings;

public class GetMyBookingsQuery: IRequest<Result<List<BookingDto>>>
{
}