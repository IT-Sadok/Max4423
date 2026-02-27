using BookingSystem.Application.Common.DTOs;
using BookingSystem.Application.Common.Interfaces.Persistence;
using MediatR;

namespace BookingSystem.Application.Features.Analytics.Queries;

public record GetMarketPricePercentilesQuery : IRequest<MarketPricePercentilesDto>;

public class GetMarketPricePercentilesQueryHandler : IRequestHandler<GetMarketPricePercentilesQuery, MarketPricePercentilesDto>
{
    private readonly IAnalyticsSqlRepository _repository;

    public GetMarketPricePercentilesQueryHandler(IAnalyticsSqlRepository repository)
    {
        _repository = repository;
    }

    public async Task<MarketPricePercentilesDto> Handle(GetMarketPricePercentilesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetMarketPricePercentilesAsync(cancellationToken);
    }
}