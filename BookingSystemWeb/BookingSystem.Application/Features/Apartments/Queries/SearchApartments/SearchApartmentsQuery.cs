using BookingSystem.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookingSystem.Application.Features.Apartments.Queries.SearchApartments;

public class SearchApartmentsQuery : IRequest<Result<PaginatedList<ApartmentDto>>>
{
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public int PageNumber { get; }
    public int PageSize { get; }

    public SearchApartmentsQuery(DateTime? checkInDate,
        DateTime? checkOutDate,
        int pageNumber = 1,
        int pageSize = 10)
    {
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize < 1 ? 10 : pageSize;
    }
}