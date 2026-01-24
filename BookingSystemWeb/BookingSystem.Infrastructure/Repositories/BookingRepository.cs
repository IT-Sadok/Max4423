using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Repositories;
using BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _context;

    public BookingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsOverlappingAsync(Guid apartmentId, DateTime start, DateTime end, CancellationToken cancellationToken = default)
    {
        return await _context.Bookings
            .AnyAsync(b =>
                    b.ApartmentId == apartmentId &&
                    b.CheckInDate < end &&
                    b.CheckOutDate > start,
                cancellationToken);
    }

    public void Add(Booking booking)
    {
        _context.Bookings.Add(booking);
    }

    public async Task<List<Booking>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Bookings
            .Include(b => b.Apartment)
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.CheckInDate)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }
}