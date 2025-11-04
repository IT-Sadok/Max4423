namespace ApartmentBookingApp
{
    internal class ApartmentService: IApartmentService
    {
        private readonly IHostService _hostService;
        private readonly IIdGeneratorService _idGeneratorService;
        private readonly IOutputWriter _outputWriter;
        private readonly IInputReader _inputReader;

        public ApartmentService(IHostService hostService, IIdGeneratorService idGeneratorService, IOutputWriter outputWriter, IInputReader inputReader)
        {
            _hostService = hostService;
            _idGeneratorService = idGeneratorService;
            _outputWriter = outputWriter;
            _inputReader = inputReader;
        }

        public void AddApartmentToHost()
        {
            if (_hostService.GetHostsCount() == 0)
            {
                _outputWriter.ShowErrorMessage("Hosts list is empty!");
                return;
            }

            int hostNumber;

            _hostService.ShowHosts();
            _outputWriter.ShowMessage($"Please, select host number(1 - {_hostService.GetHostsCount()}): ");
            while (true)
            {
                if ((int.TryParse(Console.ReadLine(), out hostNumber) && hostNumber > 0 &&
                     hostNumber <= _hostService.GetHostsCount()))
                    break;

                _outputWriter.ShowErrorMessage("Incorrect host number. Please reenter.");
            }

            var title = _inputReader.ReadStringValue("Enter apartment Title: ", v => v.Length > 0,
                "Title cannot be null or empty.");

            var pricePerNight = _inputReader.ReadDecimalValue("Enter price per night: ", v => v > 0,
                "Please, enter correct decimal value.");

            var capacity = _inputReader.ReadIntValue("Enter apartment capacity: ", v => v > 0,
                "Invalid apartment capacity. Please enter positive integer value.");

            var hosts = _hostService.GetAllHosts();
            hosts[hostNumber - 1].Apartments.Add(new Apartment()
            {
                Id = _idGeneratorService.GetNextApartmentId(),
                Title = title,
                PricePerNight = pricePerNight,
                Capacity = capacity
            });
        }

        public void ShowApartmentsByHostId(int hostId)
        {
            var host = _hostService.GetHostById(hostId);

            if (host == null)
            {
                _outputWriter.ShowErrorMessage("Hosts list is empty!");
                return;
            }

            if (host.Apartments.Count == 0)
            {
                _outputWriter.ShowErrorMessage("This is host without apartments!");
                return;
            }

            _outputWriter.ShowApartmentsList(host);
        }
    }
} 