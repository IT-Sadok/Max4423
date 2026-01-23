using BookingSystem.Domain.Entities;

namespace BookingSystem.Domain.Repositories;

public interface IBookingRepository
{
    Task<bool> IsOverlappingAsync(Guid apartmentId, DateTime start, DateTime end, CancellationToken cancellationToken = default);
    
    Task<List<Booking>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    
    void Add(Booking booking);
}