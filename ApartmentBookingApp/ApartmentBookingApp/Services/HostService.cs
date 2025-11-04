using ApartmentBookingApp.Repository;

namespace ApartmentBookingApp;

public class HostService: IHostService
{
    private readonly IHostRepository _hostRepository;
    private readonly IIdGeneratorService _idGeneratorService;
    private readonly IOutputWriter _outputWriter;
    private readonly IInputReader _inputReader;
    public HostService(IHostRepository hostRepository, IIdGeneratorService idGeneratorService, IOutputWriter outputWriter, IInputReader inputReader)
    {
        _hostRepository = hostRepository;
        _idGeneratorService = idGeneratorService;
        _outputWriter = outputWriter;
        _inputReader = inputReader;
    }

    public void AddHost()
    {
        var fullName = _inputReader.ReadStringValue("Enter host`s fullname: ", v => v.Length > 0,
            "Fullname cannot be null or empty.");

        var phoneNumber = _inputReader.ReadStringValue("Enter host`s phone number: ",
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
            _outputWriter.ShowErrorMessage("Hosts list is empty!");
            return;
        }
        _outputWriter.ShowHostsList(_hostRepository.GetAllHosts());
    }
    
    public void ShowHostById(int id)
    {
        var host = _hostRepository.GetHostById(id);
        _outputWriter.ShowMessage($"Host with Id: {host.Id} is: {host.FullName} with phone number: {host.PhoneNumber}");
    }

    public void UpdateHost()
    {
        if (GetHostsCount() == 0)
        {
            _outputWriter.ShowErrorMessage("Hosts list is empty!");
            return;
        }

        ShowHosts();
        _outputWriter.ShowMessage("Please, select host number: ");

        var hostToUpdate = GetHostById(_inputReader.ReadIntValue("Enter host number: ", v => v > 0,
            "Incorrect host number. Please reenter."));
        if (hostToUpdate == null)
        {
            _outputWriter.ShowErrorMessage("Host not found!");
            return;
        }

        var fullName = _inputReader.ReadOptionalStringValue(
            $"Enter host`s fullname(current: {hostToUpdate.FullName}) Press Enter to keep the current value.: ", v => v.Length > 0,
            "Fullname cannot be null or empty.");
        if (fullName != null)
            hostToUpdate.FullName = fullName;
        
        var phoneNumber = _inputReader.ReadOptionalStringValue(
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
                _outputWriter.ShowErrorMessage("Hosts list is empty!");
                return;
            }

            ShowHosts();
            _outputWriter.ShowMessage("Please, select host number: ");

            var hostToDelete = GetHostById(_inputReader.ReadIntValue("Enter host number: ", v => v > 0,
                "Incorrect host number. Please reenter."));
            if (hostToDelete == null)
            {
                _outputWriter.ShowErrorMessage("Host not found!");
                return;
            }
            _hostRepository.DeleteHost(hostToDelete.Id);
            _outputWriter.ShowErrorMessage($"Host with Id: {hostToDelete.Id} was successfully deleted!");
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