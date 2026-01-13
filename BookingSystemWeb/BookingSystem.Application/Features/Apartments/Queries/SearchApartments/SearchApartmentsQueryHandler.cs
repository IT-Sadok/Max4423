using BookingSystem.Application.Common.Interfaces.Data;
using BookingSystem.Domain.Common;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Features.Apartments.Queries.SearchApartments;

public class SearchApartmentsQueryHandler: IRequestHandler<SearchApartmentsQuery, Result<List<ApartmentDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SearchApartmentsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result<List<ApartmentDto>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Apartments
            .Include(a => a.Host) 
            .AsNoTracking()
            .AsQueryable();
        if (request.CheckInDate.HasValue && request.CheckOutDate.HasValue)
        {
            var start = request.CheckInDate.Value;
            var end = request.CheckOutDate.Value;
            var bookedApartmentIds = _context.Bookings
                .Where(b => b.CheckInDate < end && b.CheckOutDate > start) 
                .Select(b => b.ApartmentId);
            query = query.Where(a => !bookedApartmentIds.Contains(a.Id));
        }
        var apartments = await query
            .ToListAsync(cancellationToken);

        var apartmentsDto = _mapper.Map<List<ApartmentDto>>(apartments);

        return Result<List<ApartmentDto>>.Success(apartmentsDto);
    }
}