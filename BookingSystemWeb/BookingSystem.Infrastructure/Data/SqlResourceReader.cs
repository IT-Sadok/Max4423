using System.Reflection;

namespace BookingSystem.Infrastructure.Data;

public class SqlResourceReader
{
    public static string ReadSql(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var resourceName = assembly.GetManifestResourceNames()
            .FirstOrDefault(x => x.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));
        
        if (resourceName == null)
        {
            throw new FileNotFoundException($"SQL file '{fileName}' was not found in Embedded Resources.");
        }
        
        using var stream = assembly.GetManifestResourceStream(resourceName)!;
        using var reader = new StreamReader(stream);
        
        return reader.ReadToEnd();
    }
}