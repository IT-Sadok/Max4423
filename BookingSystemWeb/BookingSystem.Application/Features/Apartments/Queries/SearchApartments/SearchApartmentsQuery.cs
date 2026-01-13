using BookingSystem.Domain.Common;
using MediatR;

namespace BookingSystem.Application.Features.Apartments.Queries.SearchApartments;

public class SearchApartmentsQuery: IRequest<Result<List<ApartmentDto>>>
{
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
}