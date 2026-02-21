using BookingSystem.Application.Common.DTOs;
using BookingSystem.Application.Features.Apartments.Commands.UpsertApartment;

namespace BookingSystem.Application.Common.Interfaces.Persistence;

public interface IApartmentSqlRepository
{
    Task<bool> UpsertAsync(ApartmentUpsertDto dto, CancellationToken cancellationToken);
}