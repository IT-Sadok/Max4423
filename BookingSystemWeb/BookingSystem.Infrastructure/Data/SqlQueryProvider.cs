using System.Reflection;
using BookingSystem.Application.Common.Interfaces.Persistence;

namespace BookingSystem.Infrastructure.Data;

public class SqlQueryProvider : ISqlQueryProvider
{
    public string GetQuery(string queryName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        var resourceName = assembly.GetManifestResourceNames()
            .FirstOrDefault(x => x.EndsWith(queryName, StringComparison.OrdinalIgnoreCase));

        if (resourceName == null)
        {
            throw new FileNotFoundException($"SQL file '{queryName}' was not found in Embedded Resources.");
        }

        using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
        using StreamReader reader = new StreamReader(stream);
        
        return reader.ReadToEnd();
    }
}