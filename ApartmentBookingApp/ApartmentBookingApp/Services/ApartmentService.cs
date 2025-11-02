using ApartmentBookingApp.Repository;

namespace ApartmentBookingApp
{
    internal class ApartmentService: IApartmentService
    {
        private readonly IHostService _hostService;
        private readonly IIdGeneratorService _idGeneratorService;

        public ApartmentService(IHostService hostService, IIdGeneratorService idGeneratorService)
        {
            _hostService = hostService;
            _idGeneratorService = idGeneratorService;
        }

        public void AddApartmentToHost()
        {
            if (_hostService.GetHostsCount() == 0)
            {
                Console.WriteLine("Hosts list is empty!");
                return;
            }

            int hostNumber;

            _hostService.ShowHosts();
            Console.Write($"Please, select host number(1 - {_hostService.GetHostsCount()}): ");
            while (true)
            {
                if ((int.TryParse(Console.ReadLine(), out hostNumber) && hostNumber > 0 &&
                     hostNumber <= _hostService.GetHostsCount()))
                    break;

                Console.WriteLine("Incorrect host number. Please reenter.");
            }

            var title = ConsoleInputReader.ReadStringValue("Enter apartment Title: ", v => v.Length > 0,
                "Title cannot be null or empty.");

            var pricePerNight = ConsoleInputReader.ReadDecimalValue("Enter price per night: ", v => v > 0,
                "Please, enter correct decimal value.");

            var capacity = ConsoleInputReader.ReadIntValue("Enter apartment capacity: ", v => v > 0,
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
                Console.WriteLine("Hosts list is empty!");
                return;
            }

            if (host.Apartments.Count == 0)
            {
                Console.WriteLine("This is host without apartments!");
                return;
            }

            for (int i = 0; i < host.Apartments.Count(); i++)
            {
                var apartment = host.Apartments[i];
                Console.WriteLine(
                    $"{i + 1}. Id: {apartment.Id} | Title: {apartment.Title} | PricePerNight: {apartment.PricePerNight} | Capacity: {apartment.Capacity}");
            }
        }
    }
}