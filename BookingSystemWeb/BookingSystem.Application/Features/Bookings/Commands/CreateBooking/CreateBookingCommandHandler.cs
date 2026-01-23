using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Domain.Common;
using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Repositories;
using MediatR;

namespace BookingSystem.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<Guid>>
{
    private readonly IApartmentRepository _apartmentRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public CreateBookingCommandHandler(
        IApartmentRepository apartmentRepository,
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
    {
        _apartmentRepository = apartmentRepository;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Guid>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null)
        {
            return Result<Guid>.Failure("User not found.");
        }

        var apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId, cancellationToken);
        if (apartment == null)
        {
            return Result<Guid>.Failure("Apartment not found.");
        }
        
        var isOverlapping = await _bookingRepository.IsOverlappingAsync(
            request.ApartmentId, 
            request.CheckInDate, 
            request.CheckOutDate, 
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
        
        _bookingRepository.Add(booking);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<Guid>.Success(booking.Id);
    }
}