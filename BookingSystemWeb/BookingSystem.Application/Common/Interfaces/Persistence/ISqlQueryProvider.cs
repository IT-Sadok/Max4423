namespace BookingSystem.Application.Common.Interfaces.Persistence;

public interface ISqlQueryProvider
{
    string GetQuery(string queryName);
}