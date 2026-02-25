using BookingSystem.Application.Common.DTOs;
using BookingSystem.Application.Common.Interfaces.Persistence;
using MediatR;

namespace BookingSystem.Application.Features.Analytics.Queries;

public record GetApartmentOccupancyQuery(
    DateTime StartDate,
    DateTime EndDate,
    int PageNumber,
    int PageSize) : IRequest<IEnumerable<ApartmentOccupancyDto>>;

public class
    GetApartmentOccupancyQueryHandler : IRequestHandler<GetApartmentOccupancyQuery, IEnumerable<ApartmentOccupancyDto>>
{
    private readonly IAnalyticsSqlRepository _repository;

    public GetApartmentOccupancyQueryHandler(IAnalyticsSqlRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ApartmentOccupancyDto>> Handle(GetApartmentOccupancyQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetApartmentOccupancyAsync(request.StartDate, request.EndDate, request.PageNumber,
            request.PageSize, cancellationToken);
    }
}