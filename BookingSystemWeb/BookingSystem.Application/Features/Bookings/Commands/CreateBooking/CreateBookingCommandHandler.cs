using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Application.Common.Interfaces.Data;
using BookingSystem.Domain.Common;
using BookingSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateBookingCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Guid>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null)
        {
            return Result<Guid>.Failure("User not found.");
        }

        var apartment = await _context.Apartments
            .FirstOrDefaultAsync(a => a.Id == request.ApartmentId, cancellationToken);
        if (apartment == null)
        {
            return Result<Guid>.Failure("Apartment not found.");
        }

        var isOverlapping = await _context.Bookings
            .AnyAsync(b =>
                    b.ApartmentId == request.ApartmentId &&
                    b.CheckInDate < request.CheckOutDate &&
                    b.CheckOutDate > request.CheckInDate,
                cancellationToken);

        if (isOverlapping)
        {
            return Result<Guid>.Failure("These dates are already booked");
        }
        
        var booking = Booking.Reserve(
            apartment, 
            userId.Value, 
            request.CheckInDate, 
            request.CheckOutDate);
        
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(booking.Id);
    }
}