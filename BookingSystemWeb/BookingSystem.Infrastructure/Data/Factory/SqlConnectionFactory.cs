using System.Data;
using BookingSystem.Application.Common.Interfaces.Persistence;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace BookingSystem.Infrastructure.Data.Factory;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}