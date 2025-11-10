using System.Text.Json;

namespace ApartmentBookingApp.Repository;

public class HostRepository : IHostRepository
{
    private const string FilePath = "hosts.json";
    private List<Host> _hosts;

    public HostRepository()
    {
        if (!File.Exists(FilePath))
        {
            _hosts = new List<Host>();
            return;
        }

        string json;
        using (StreamReader reader = new StreamReader(FilePath))
        {
            json = reader.ReadToEnd();
        }

        if (string.IsNullOrEmpty(json))
        {
            _hosts = new List<Host>();
            return;
        }

        try
        {
            _hosts = JsonSerializer.Deserialize<List<Host>>(json) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            Console.WriteLine("Cannot read from json file. Exception: " + e.Message);
            _hosts = new List<Host>();
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
}