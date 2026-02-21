using BookingSystem.Application.Common.DTOs;
using BookingSystem.Application.Common.Interfaces.Persistence;
using MediatR;

namespace BookingSystem.Application.Features.Apartments.Commands.UpsertApartment;

public class UpsertApartmentCommandHandler : IRequestHandler<UpsertApartmentCommand, bool>
{
    private readonly IApartmentSqlRepository _sqlRepository;

    public UpsertApartmentCommandHandler(IApartmentSqlRepository sqlRepository)
    {
        _sqlRepository = sqlRepository;
    }

    public async Task<bool> Handle(UpsertApartmentCommand request, CancellationToken cancellationToken)
    {
        var dto = new ApartmentUpsertDto
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            Address = request.Address,
            PricePerNight = request.PricePerNight,
            ExternalId = request.ExternalId,
            HostId = request.HostId,
            CustomData = request.CustomData
        };

        return await _sqlRepository.UpsertAsync(dto, cancellationToken);
    }
}