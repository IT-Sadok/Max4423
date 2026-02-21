using MediatR;

namespace BookingSystem.Application.Features.Apartments.Commands.UpsertApartment;

public record UpsertApartmentCommand(
    Guid Id,
    string Title,
    string Description,
    string Address,
    decimal PricePerNight,
    string ExternalId,
    Guid HostId,
    string? CustomData
) : IRequest<bool>;