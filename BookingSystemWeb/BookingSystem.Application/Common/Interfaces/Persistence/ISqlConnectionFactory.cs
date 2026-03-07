using System.Data;

namespace BookingSystem.Application.Common.Interfaces.Persistence;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}