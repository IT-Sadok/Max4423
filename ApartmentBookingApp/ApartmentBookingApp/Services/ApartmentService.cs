namespace ApartmentBookingApp
{
    internal class ApartmentService(
        IHostService hostService,
        IIdGeneratorService idGeneratorService,
        IOutputWriter outputWriter,
        IInputReader inputReader)
        : IApartmentService
    {
        public void AddApartmentToHost()
        {
            if (hostService.GetHostsCount() == 0)
            {
                outputWriter.ShowErrorMessage("Hosts list is empty!");
                return;
            }

            int hostNumber;

            hostService.ShowHosts();
            outputWriter.ShowMessage($"Please, select host number(1 - {hostService.GetHostsCount()}): ");
            while (true)
            {
                if ((int.TryParse(Console.ReadLine(), out hostNumber) && hostNumber > 0 &&
                     hostNumber <= hostService.GetHostsCount()))
                    break;

                outputWriter.ShowErrorMessage("Incorrect host number. Please reenter.");
            }

            var title = inputReader.ReadStringValue("Enter apartment Title: ", v => v.Length > 0,
                "Title cannot be null or empty.");

            var pricePerNight = inputReader.ReadDecimalValue("Enter price per night: ", v => v > 0,
                "Please, enter correct decimal value.");

            var capacity = inputReader.ReadIntValue("Enter apartment capacity: ", v => v > 0,
                "Invalid apartment capacity. Please enter positive integer value.");

            var hosts = hostService.GetAllHosts();
            hosts[hostNumber - 1].Apartments.Add(new Apartment()
            {
                Id = idGeneratorService.GetNextApartmentId(),
                Title = title,
                PricePerNight = pricePerNight,
                Capacity = capacity
            });
        }

        public void ShowApartmentsByHostId(int hostId)
        {
            var host = hostService.GetHostById(hostId);

            if (host == null)
            {
                outputWriter.ShowErrorMessage("Hosts list is empty!");
                return;
            }

            if (host.Apartments.Count == 0)
            {
                outputWriter.ShowErrorMessage("This is host without apartments!");
                return;
            }

            outputWriter.ShowApartmentsList(host);
        }
    }
} 