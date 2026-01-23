using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Domain.Common;
using BookingSystem.Domain.Repositories;
using MapsterMapper;
using MediatR;

namespace BookingSystem.Application.Features.Bookings.Queries.GetMyBookings;

public class GetMyBookingsQueryHandler : IRequestHandler<GetMyBookingsQuery, Result<List<BookingDto>>>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetMyBookingsQueryHandler(
        IBookingRepository bookingRepository, 
        ICurrentUserService currentUserService, 
        IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<Result<List<BookingDto>>> Handle(GetMyBookingsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
        {
            return Result<List<BookingDto>>.Failure("User is not authorized");
        }
        
        var bookings = await _bookingRepository.GetByUserIdAsync(userId.Value, cancellationToken);
        
        var bookingsDto = _mapper.Map<List<BookingDto>>(bookings);

        return Result<List<BookingDto>>.Success(bookingsDto);
    }
}