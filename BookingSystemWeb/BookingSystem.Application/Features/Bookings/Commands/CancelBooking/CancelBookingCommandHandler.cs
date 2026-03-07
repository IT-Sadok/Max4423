using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Domain.Common;
using BookingSystem.Domain.Repositories;
using MediatR;

namespace BookingSystem.Application.Features.Bookings.Commands.CancelBooking;

public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, Result<bool>>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public CancelBookingCommandHandler(IBookingRepository bookingRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(request.BookingId, cancellationToken );
        var userId = _currentUserService.UserId;

        if (booking == null || userId == null || booking.UserId != userId)
        {
            return Result<bool>.Failure("Booking not found.");
        }

        booking.Cancel();
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}