namespace ApartmentBookingApp.Repository;

public interface IHostRepository
{
    void AddHost(Host host);
    Host GetHostById(int hostId);
    List<Host> GetAllHosts();
    void UpdateHost(Host hostToUpdate);
    void DeleteHost(int hostId);
    int HostsCount();
    void SaveChanges();

}