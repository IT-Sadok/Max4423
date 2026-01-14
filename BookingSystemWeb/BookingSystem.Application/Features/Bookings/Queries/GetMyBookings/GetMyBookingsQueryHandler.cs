using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Application.Common.Interfaces.Data;
using BookingSystem.Domain.Common;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Features.Bookings.Queries.GetMyBookings;

public class GetMyBookingsQueryHandler: IRequestHandler<GetMyBookingsQuery,Result<List<BookingDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetMyBookingsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
    {
        _context = context;
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
        
        var bookings = await _context.Bookings
            .Include(b => b.Apartment)
            .Where(b => b.UserId == userId)
            .AsNoTracking()
            .OrderByDescending(b => b.CheckInDate)
            .ToListAsync(cancellationToken);
        
        var bookingsDto = _mapper.Map<List<BookingDto>>(bookings);

        return Result<List<BookingDto>>.Success(bookingsDto);
    }
}