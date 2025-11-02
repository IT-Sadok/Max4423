namespace ApartmentBookingApp;

public interface IHostService
{
    void AddHost();
    void ShowHosts();
    Host GetHostById(int hostId);
    List<Host> GetAllHosts();
    int GetHostsCount();
}