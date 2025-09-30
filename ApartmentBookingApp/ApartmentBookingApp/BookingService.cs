namespace ApartmentBookingApp
{
    internal class BookingService
    {
        private List<Host> _hosts = new List<Host>();
        private IdGeneratorService _idGeneratorService = new IdGeneratorService();

        public void AddHost()
        {
            var fullName = InputHelper.ReadStringValue("Enter host`s fullname: ", v => v.Length > 0,
                "Fullname cannot be null or empty.");

            var phoneNumber = InputHelper.ReadStringValue("Enter host`s phone number: ",
                v => v.Length > 5 && v.Length < 15, "Phone number cannot be < 5 and > 15 symbols");

            _hosts.Add(new Host()
                { Id = _idGeneratorService.GetNextHostId(), FullName = fullName, PhoneNumber = phoneNumber });
        }

        public void ShowHosts()
        {
            if (_hosts.Count == 0)
            {
                Console.WriteLine("_hosts list is empty!");
                return;
            }

            for (int i = 0; i < _hosts.Count; i++)
            {
                var host = _hosts[i];
                Console.WriteLine(
                    $"[{i + 1}]. Id: {host.Id} | FullName: {host.FullName} | PhoneNumber: {host.PhoneNumber}");
            }
        }

        public void AddApartmentToHost()
        {
            if (_hosts.Count == 0)
            {
                Console.WriteLine("_hosts list is empty!");
                return;
            }

            int hostNumber;

            ShowHosts();
            Console.Write($"Please, select host number(1 - {_hosts.Count}): ");
            while (true)
            {
                if ((int.TryParse(Console.ReadLine(), out hostNumber) && hostNumber > 0 && hostNumber <= _hosts.Count))
                    break;

                Console.WriteLine("Incorrect host number. Please reenter.");
            }

            var title = InputHelper.ReadStringValue("Enter apartment Title: ", v => v.Length > 0,
                "Title cannot be null or empty.");

            var pricePerNight = InputHelper.ReadDecimalValue("Enter price per night: ", v => v > 0,
                "Please, enter correct decimal value.");

            var capacity = InputHelper.ReadIntValue("Enter apartment capacity: ", v => v > 0,
                "Invalid apartment capacity. Please enter positive integer value.");

            _hosts[hostNumber - 1].Apartments.Add(new Apartment()
            {
                Id = _idGeneratorService.GetNextApartamentId(), Title = title, PricePerNight = pricePerNight,
                Capacity = capacity
            });
        }

        public void ShowApartmentByHostId(int hostId)
        {
            if (_hosts.Count == 0)
            {
                Console.WriteLine("Hosts list is empty!");
                return;
            }

            if (_hosts[hostId - 1].Apartments.Count == 0)
            {
                Console.WriteLine("This is host without apartments!");
                return;
            }

            for (int i = 0; i < _hosts[hostId - 1].Apartments.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. Id: {_hosts[hostId - 1].Apartments[i].Id} | Title: {_hosts[hostId - 1].Apartments[i].Title} | PricePerNight: {_hosts[hostId - 1].Apartments[i].PricePerNight} | Capacity: {_hosts[hostId - 1].Apartments[i].Capacity}");
            }
        }

        public int HostsCount()
        {
            return _hosts.Count;
        }
    }
}