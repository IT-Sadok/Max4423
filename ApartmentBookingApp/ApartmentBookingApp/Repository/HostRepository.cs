namespace ApartmentBookingApp.Repository;

public class HostRepository: IHostRepository
{
    private List<Host> _hosts = new List<Host>();

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
}