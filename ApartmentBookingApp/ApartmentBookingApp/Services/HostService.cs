using ApartmentBookingApp.Repository;
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

namespace ApartmentBookingApp;

public class HostService(
    IHostRepository hostRepository,
    IIdGeneratorService idGeneratorService,
    IOutputWriter outputWriter,
    IInputReader inputReader)
    : IHostService
{
    public void AddHost()
    {
        var fullName = inputReader.ReadStringValue("Enter host`s fullname: ", v => v.Length > 0,
            "Fullname cannot be null or empty.");

        var phoneNumber = inputReader.ReadStringValue("Enter host`s phone number: ",
            v => v.Length is > 5 and < 15, "Phone number cannot be < 5 and > 15 symbols");

        var newHost = new Host()
        {
            Id = idGeneratorService.GetNextHostId(),
            FullName = fullName,
            PhoneNumber = phoneNumber
        };
        hostRepository.AddHost(newHost);
    }

    public void ShowHosts()
    {
        var hosts = hostRepository.GetAllHosts();
        if (hostRepository.HostsCount() == 0)
        {
            outputWriter.ShowErrorMessage("Hosts list is empty!");
            return;
        }
        outputWriter.ShowHostsList(hosts);
    }
    
    public void ShowHostById(int id)
    {
        var host = hostRepository.GetHostById(id);
        outputWriter.ShowMessage($"Host with Id: {host.Id} is: {host.FullName} with phone number: {host.PhoneNumber}");
    }

    public void UpdateHost()
    {
        if (GetHostsCount() == 0)
        {
            outputWriter.ShowErrorMessage("Hosts list is empty!");
            return;
        }

        ShowHosts();
        outputWriter.ShowMessage("Please, select host number: ");

        var hostToUpdate = GetHostById(inputReader.ReadIntValue("Enter host number: ", v => v > 0,
            "Incorrect host number. Please reenter."));
        if (hostToUpdate == null)
        {
            outputWriter.ShowErrorMessage("Host not found!");
            return;
        }

        var fullName = inputReader.ReadOptionalStringValue(
            $"Enter host`s fullname(current: {hostToUpdate.FullName}) Press Enter to keep the current value.: ", v => v.Length > 0,
            "Fullname cannot be null or empty.");
        if (fullName != null)
            hostToUpdate.FullName = fullName;
        
        var phoneNumber = inputReader.ReadOptionalStringValue(
                $"Enter host`s phone number(current: {hostToUpdate.PhoneNumber}) Press Enter to keep the current value.: ",
                v => v.Length is > 5 and < 15, "Phone number cannot be < 5 and > 15 symbols");
       
        if (phoneNumber != null)
            hostToUpdate.PhoneNumber = phoneNumber;
        
        hostRepository.UpdateHost(hostToUpdate);
        }

        public void DeleteHost()
        {
            if (GetHostsCount() == 0)
            {
                outputWriter.ShowErrorMessage("Hosts list is empty!");
                return;
            }

            ShowHosts();
            outputWriter.ShowMessage("Please, select host number: ");

            var hostToDelete = GetHostById(inputReader.ReadIntValue("Enter host number: ", v => v > 0,
                "Incorrect host number. Please reenter."));
            if (hostToDelete == null)
            {
                outputWriter.ShowErrorMessage("Host not found!");
                return;
            }
            hostRepository.DeleteHost(hostToDelete.Id);
            outputWriter.ShowErrorMessage($"Host with Id: {hostToDelete.Id} was successfully deleted!");
        }

        public Host GetHostById(int hostId)
        {
            return hostRepository.GetHostById(hostId);
        }

        public List<Host> GetAllHosts()
        {
            return hostRepository.GetAllHosts();
        }

        public int GetHostsCount()
        {
            return hostRepository.HostsCount();
        }
    }