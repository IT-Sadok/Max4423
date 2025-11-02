namespace ApartmentBookingApp;

public interface IHostService
{
    void AddHost();
    void ShowHosts();
    void UpdateHost();
    void DeleteHost();
    Host GetHostById(int hostId);
    List<Host> GetAllHosts();
    int GetHostsCount();
}