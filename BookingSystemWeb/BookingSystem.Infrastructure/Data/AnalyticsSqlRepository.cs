using BookingSystem.Application.Common.DTOs;
using BookingSystem.Application.Common.Interfaces.Persistence;
using Dapper;

namespace BookingSystem.Infrastructure.Data;

public class AnalyticsSqlRepository : IAnalyticsSqlRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly ISqlQueryProvider _queryProvider;

    public AnalyticsSqlRepository(ISqlConnectionFactory connectionFactory, ISqlQueryProvider queryProvider)
    {
        _connectionFactory = connectionFactory;
        _queryProvider = queryProvider;
    }

    public async Task<IEnumerable<HostPortfolioDto>> GetHostsPortfolioAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var sql = _queryProvider.GetQuery("GetHostsPortfolio.sql");
        using var connection = _connectionFactory.CreateConnection();
        var parameters = new 
        { 
            Limit = pageSize,
            Offset = (pageNumber - 1) * pageSize 
        };
        
        return await connection.QueryAsync<HostPortfolioDto>(new CommandDefinition(sql, parameters,cancellationToken: cancellationToken));
    }

    public async Task<IEnumerable<TopHostPerformanceDto>> GetTopHostsPerformanceAsync(
        CancellationToken cancellationToken = default)
    {
        var sql = _queryProvider.GetQuery("GetTopHostsPerformance.sql");
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<TopHostPerformanceDto>(new CommandDefinition(sql,cancellationToken: cancellationToken));
    }

    public async Task<IEnumerable<MonthlyDynamicsDto>> GetMonthlyDynamicsAsync(
        CancellationToken cancellationToken = default)
    {
        var sql = _queryProvider.GetQuery("GetMonthlyDynamics.sql");
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<MonthlyDynamicsDto>(new CommandDefinition(sql,cancellationToken: cancellationToken));
    }

    public async Task<MarketPricePercentilesDto> GetMarketPricePercentilesAsync(
        CancellationToken cancellationToken = default)
    {
        var sql = _queryProvider.GetQuery("GetMarketPricePercentiles.sql");
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleAsync<MarketPricePercentilesDto>(
            new CommandDefinition(sql, cancellationToken: cancellationToken));
    }

    public async Task<IEnumerable<ApartmentOccupancyDto>> GetApartmentOccupancyAsync(DateTime startDate,
        DateTime endDate, int pageNumber,
        int pageSize, CancellationToken cancellationToken = default)
    {
        var sql = _queryProvider.GetQuery("GetApartmentOccupancy.sql");
        using var connection = _connectionFactory.CreateConnection();

        var parameters = new
            { StartDate = startDate, EndDate = endDate, Offset = (pageNumber - 1) * pageSize, Limit = pageSize };

        return await connection.QueryAsync<ApartmentOccupancyDto>(new CommandDefinition(sql, parameters,
            cancellationToken: cancellationToken));
    }
}