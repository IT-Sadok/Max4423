using BookingSystem.Application.Common.DTOs;
using BookingSystem.Application.Common.Interfaces.Persistence;
using MediatR;

namespace BookingSystem.Application.Features.Analytics.Queries;

public record GetHostPortfolioQuery(int PageNumber = 1, int PageSize = 10) : IRequest<IEnumerable<HostPortfolioDto>>;

public class GetHostPortfolioQueryHandler : IRequestHandler<GetHostPortfolioQuery, IEnumerable<HostPortfolioDto>>
{
    private readonly IAnalyticsSqlRepository _repository;

    public GetHostPortfolioQueryHandler(IAnalyticsSqlRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<HostPortfolioDto>> Handle(GetHostPortfolioQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetHostsPortfolioAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}