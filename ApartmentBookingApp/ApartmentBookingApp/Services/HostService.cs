using ApartmentBookingApp.Repository;

namespace ApartmentBookingApp;

public class HostService: IHostService
{
    private readonly IHostRepository _hostRepository;
    private readonly IIdGeneratorService _idGeneratorService;

    public HostService(IHostRepository hostRepository, IIdGeneratorService idGeneratorService)
    {
        _hostRepository = hostRepository;
        _idGeneratorService = idGeneratorService;
    }
    public void AddHost()
    {
        var fullName = ConsoleInputReader.ReadStringValue("Enter host`s fullname: ", v => v.Length > 0,
            "Fullname cannot be null or empty.");

        var phoneNumber = ConsoleInputReader.ReadStringValue("Enter host`s phone number: ",
            v => v.Length > 5 && v.Length < 15, "Phone number cannot be < 5 and > 15 symbols");

        var newHost = new Host()
        {
            Id = _idGeneratorService.GetNextHostId(),
            FullName = fullName,
            PhoneNumber = phoneNumber
        };
        _hostRepository.AddHost(newHost);
    }

    public void ShowHosts()
    {
        var hosts = _hostRepository.GetAllHosts();
        if (_hostRepository.HostsCount() == 0)
        {
            Console.WriteLine("_hosts list is empty!");
            return;
        }
        for (int i = 0; i < _hostRepository.HostsCount(); i++)
        {
            var host = hosts[i];
            Console.WriteLine(
                $"[{i + 1}]. Id: {host.Id} | FullName: {host.FullName} | PhoneNumber: {host.PhoneNumber}");
        }
    }

    public Host GetHostById(int hostId)
    {
        return _hostRepository.GetHostById(hostId);       
    }

    public List<Host> GetAllHosts()
    {
        return _hostRepository.GetAllHosts();
    }

    public int GetHostsCount()
    {
        return _hostRepository.HostsCount();
    }
}