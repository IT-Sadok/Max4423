using BookingSystem.Domain.Common;
using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Repositories;
using BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Repositories;

public class ApartmentRepository : IApartmentRepository
{
    private readonly ApplicationDbContext _context;

    public ApartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Apartment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Apartments
            .Include(a => a.Host)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<PaginatedList<Apartment>> SearchAvailableAsync(
        DateTime? start, 
        DateTime? end, 
        int pageNumber,
        int pageSize, 
        CancellationToken cancellationToken = default)
    {
        var query = _context.Apartments
            .Include(a => a.Host)
            .AsNoTracking()
            .AsQueryable();

        if (start.HasValue && end.HasValue)
        {
            var bookedApartmentIds = _context.Bookings
                .Where(b => b.CheckInDate < end && b.CheckOutDate > start)
                .Select(b => b.ApartmentId);
        
            query = query.Where(a => !bookedApartmentIds.Contains(a.Id));
        }

        var count = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<Apartment>(items, count, pageNumber, pageSize);
    }
}