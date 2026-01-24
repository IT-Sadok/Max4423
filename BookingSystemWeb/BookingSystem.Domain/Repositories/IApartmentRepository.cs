using BookingSystem.Domain.Common;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Domain.Repositories;

public interface IApartmentRepository
{
    Task<Apartment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PaginatedList<Apartment>> SearchAvailableAsync(
        DateTime? start,
        DateTime? end,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
}