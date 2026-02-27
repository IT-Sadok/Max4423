using BookingSystem.Application.Common.DTOs;
using BookingSystem.Application.Common.Interfaces.Persistence;
using MediatR;

namespace BookingSystem.Application.Features.Analytics.Queries;

public record GetTopHostPerformanceQuery : IRequest<IEnumerable<TopHostPerformanceDto>>;

public class GetTopHostPerformanceQueryHandler : IRequestHandler<GetTopHostPerformanceQuery, IEnumerable<TopHostPerformanceDto>>
{
    private readonly IAnalyticsSqlRepository _repository;
    
    public GetTopHostPerformanceQueryHandler(IAnalyticsSqlRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<TopHostPerformanceDto>> Handle(GetTopHostPerformanceQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetTopHostsPerformanceAsync(cancellationToken);
    }
}