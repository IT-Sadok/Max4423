using System.Text.Json;

namespace ApartmentBookingApp.Repository;

public class HostRepository : IHostRepository
{
    private const string FilePath = "hosts.json";
    private List<Host> _hosts;

    private HostRepository(List<Host> hosts)
    {
        _hosts = hosts;
    }

    public static HostRepository Create()
    {
        if (!File.Exists(FilePath))
        {
            return new HostRepository(new List<Host>());
        }

        string json;
        using (StreamReader reader = new StreamReader(FilePath))
        {
            json = reader.ReadToEnd();
        }

        if (string.IsNullOrEmpty(json))
        {
            return new HostRepository(new List<Host>());
        }

        try
        {
            var loadedHosts = JsonSerializer.Deserialize<List<Host>>(json);
            if (loadedHosts == null)
            {
                return new HostRepository(new List<Host>());
            }

            return new HostRepository(loadedHosts);
        }
        catch (Exception e)
        {
            Console.WriteLine("Cannot read from json file. Exception: " + e.Message);
            return new HostRepository(new List<Host>());
        }
    }

    public void AddHost(Host host)
    {
        _hosts.Add(host);
    }

    public Host GetHostById(int hostId)
    {
        return _hosts.FirstOrDefault(x => x.Id == hostId);
    }

    public List<Host> GetAllHosts()
    {
        return _hosts.ToList();
    }

    public void UpdateHost(Host hostToUpdate)
    {
        int index = _hosts.FindIndex(x => x.Id == hostToUpdate.Id);

        if (index != -1)
            _hosts[index] = hostToUpdate;
    }

    public void DeleteHost(int hostId)
    {
        var hostToRemove = GetHostById(hostId);

        if (hostToRemove != null)
            _hosts.Remove(hostToRemove);
    }

    public int HostsCount()
    {
        return _hosts.Count;
    }

    public void SaveChanges()
    {
        string json = JsonSerializer.Serialize(_hosts, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        using (StreamWriter writer = new StreamWriter(FilePath))
        {
            writer.Write(json);
        }
    }

    public int GetMaxHostId()
    {
        if (_hosts.Count <= 0)
            return 0;
        return _hosts.Max(x => x.Id);
    }

    public int GetMaxApartmentId()
    {
        var allApartments = _hosts.SelectMany(host => host.Apartments);
        if (!allApartments.Any())
            return 0;
        return allApartments.Max(x => x.Id);
    }
}