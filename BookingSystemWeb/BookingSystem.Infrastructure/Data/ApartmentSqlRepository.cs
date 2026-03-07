using BookingSystem.Application.Common.DTOs;
using BookingSystem.Application.Common.Interfaces.Persistence;
using BookingSystem.Application.Features.Apartments.Commands.UpsertApartment;
using Dapper;

namespace BookingSystem.Infrastructure.Data;

public class ApartmentSqlRepository : IApartmentSqlRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly ISqlQueryProvider _queryProvider;

    public ApartmentSqlRepository(
        ISqlConnectionFactory connectionFactory,
        ISqlQueryProvider queryProvider)
    {
        _connectionFactory = connectionFactory;
        _queryProvider = queryProvider;
    }

    public async Task<Guid> UpsertAsync(ApartmentUpsertDto dto, CancellationToken cancellationToken)
    {
        var sql = _queryProvider.GetQuery("UpsertApartment.sql");

        using var connection = _connectionFactory.CreateConnection();

        var commandDefinition = new CommandDefinition(
            sql,
            parameters: dto,
            cancellationToken: cancellationToken);

        var resultId = await connection.QuerySingleOrDefaultAsync<Guid>(commandDefinition);
        return resultId;
    }
}