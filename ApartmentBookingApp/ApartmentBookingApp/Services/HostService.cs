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
            Console.WriteLine("Hosts list is empty!");
            return;
        }
        for (int i = 0; i < GetHostsCount(); i++)
        {
            var host = hosts[i];
            Console.WriteLine(
                $"[{i + 1}]. Id: {host.Id} | FullName: {host.FullName} | PhoneNumber: {host.PhoneNumber}");
        }
    }

    public void UpdateHost()
    {
        if (GetHostsCount() == 0)
        {
            Console.WriteLine("Hosts list is empty!");
            return;
        }

        ShowHosts();
        Console.WriteLine("Please, select host number: ");

        var hostToUpdate = GetHostById(ConsoleInputReader.ReadIntValue("Enter host number: ", v => v > 0,
            "Incorrect host number. Please reenter."));
        if (hostToUpdate == null)
        {
            Console.WriteLine("Host not found!");
            return;
        }

        var fullName = ConsoleInputReader.ReadOptionalStringValue(
            $"Enter host`s fullname(current: {hostToUpdate.FullName}) Press Enter to keep the current value.: ", v => v.Length > 0,
            "Fullname cannot be null or empty.");
        if (fullName != null)
            hostToUpdate.FullName = fullName;
        
        var phoneNumber = ConsoleInputReader.ReadOptionalStringValue(
                $"Enter host`s phone number(current: {hostToUpdate.PhoneNumber}) Press Enter to keep the current value.: ",
                v => v.Length > 5 && v.Length < 15, "Phone number cannot be < 5 and > 15 symbols");
       
        if (phoneNumber != null)
            hostToUpdate.PhoneNumber = phoneNumber;
        
        _hostRepository.UpdateHost(hostToUpdate);
        }

        public void DeleteHost()
        {
            if (GetHostsCount() == 0)
            {
                Console.WriteLine("Hosts list is empty!");
                return;
            }

            ShowHosts();
            Console.WriteLine("Please, select host number: ");

            var hostToDelete = GetHostById(ConsoleInputReader.ReadIntValue("Enter host number: ", v => v > 0,
                "Incorrect host number. Please reenter."));
            if (hostToDelete == null)
            {
                Console.WriteLine("Host not found!");
                return;
            }
            _hostRepository.DeleteHost(hostToDelete.Id);
            Console.WriteLine($"Host with Id: {hostToDelete.Id} was successfully deleted!");
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