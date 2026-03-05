using BookingSystem.Application.Common.DTOs;

namespace BookingSystem.Application.Common.Interfaces.Persistence;

public interface IAnalyticsSqlRepository
{
    Task<IEnumerable<HostPortfolioDto>> GetHostsPortfolioAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<IEnumerable<TopHostPerformanceDto>> GetTopHostsPerformanceAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<MonthlyDynamicsDto>> GetMonthlyDynamicsAsync(CancellationToken cancellationToken = default);
    Task<MarketPricePercentilesDto> GetMarketPricePercentilesAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<ApartmentOccupancyDto>> GetApartmentOccupancyAsync(DateTime? startDate, DateTime? endDate, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}