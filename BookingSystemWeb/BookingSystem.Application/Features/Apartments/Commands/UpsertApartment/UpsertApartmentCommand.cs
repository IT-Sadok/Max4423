using System.Text.Json.Serialization;
using MediatR;

namespace BookingSystem.Application.Features.Apartments.Commands.UpsertApartment;

public record UpsertApartmentCommand(
    string Title,
    string Description,
    string Address,
    decimal PricePerNight,
    string ExternalId,
    [property: JsonIgnore]
    Guid HostId,
    string? CustomData
) : IRequest<Guid>;