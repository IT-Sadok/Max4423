using BookingSystem.Domain.Common;
using BookingSystem.Domain.Repositories;
using MapsterMapper;
using MediatR;

namespace BookingSystem.Application.Features.Apartments.Queries.SearchApartments;

public class SearchApartmentsQueryHandler : IRequestHandler<SearchApartmentsQuery, Result<List<ApartmentDto>>>
{
    private readonly IApartmentRepository _apartmentRepository;
    private readonly IMapper _mapper;

    public SearchApartmentsQueryHandler(IApartmentRepository apartmentRepository, IMapper mapper)
    {
        _apartmentRepository = apartmentRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ApartmentDto>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
    {
        var apartments = await _apartmentRepository.SearchAvailableAsync(
            request.CheckInDate, 
            request.CheckOutDate, 
            cancellationToken);

        var apartmentsDto = _mapper.Map<List<ApartmentDto>>(apartments);

        return Result<List<ApartmentDto>>.Success(apartmentsDto);
    }
}