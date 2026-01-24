using BookingSystem.Domain.Common;
using BookingSystem.Domain.Repositories;
using MapsterMapper;
using MediatR;

namespace BookingSystem.Application.Features.Apartments.Queries.SearchApartments;

public class SearchApartmentsQueryHandler : IRequestHandler<SearchApartmentsQuery, Result<PaginatedList<ApartmentDto>>>
{
    private readonly IApartmentRepository _apartmentRepository;
    private readonly IMapper _mapper;

    public SearchApartmentsQueryHandler(IApartmentRepository apartmentRepository, IMapper mapper)
    {
        _apartmentRepository = apartmentRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<ApartmentDto>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
    {
        var paginatedApartments = await _apartmentRepository.SearchAvailableAsync(
            request.CheckInDate, 
            request.CheckOutDate, 
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        var apartmentDtos = _mapper.Map<List<ApartmentDto>>(paginatedApartments.Items);
        
        var result = new PaginatedList<ApartmentDto>(
            apartmentDtos, 
            paginatedApartments.TotalCount, 
            paginatedApartments.PageNumber, 
            request.PageSize);

        return Result<PaginatedList<ApartmentDto>>.Success(result);
    }
}