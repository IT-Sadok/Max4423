using BookingSystem.Domain.Entities;

namespace BookingSystem.Domain.Repositories;

public interface IApartmentRepository
{
    Task<Apartment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<Apartment>> SearchAvailableAsync(DateTime? start, DateTime? end,
        CancellationToken cancellationToken = default);
}