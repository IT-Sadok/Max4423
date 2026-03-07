using BookingSystem.Application.Common.DTOs;
using BookingSystem.Application.Common.Interfaces.Persistence;
using MediatR;

namespace BookingSystem.Application.Features.Analytics.Queries;

public record GetMonthlyDynamicsQuery : IRequest<IEnumerable<MonthlyDynamicsDto>>;

public class GetMonthlyDynamicsQueryHandler : IRequestHandler<GetMonthlyDynamicsQuery, IEnumerable<MonthlyDynamicsDto>>
{
    private readonly IAnalyticsSqlRepository _repository;

    public GetMonthlyDynamicsQueryHandler(IAnalyticsSqlRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<MonthlyDynamicsDto>> Handle(GetMonthlyDynamicsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetMonthlyDynamicsAsync(cancellationToken);
    }
}

