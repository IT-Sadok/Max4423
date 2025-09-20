namespace ApartmentBookingApp
{
    internal class BookingSystem
    {
        static List<Host> _hosts = new List<Host>();

        public static void AddHost()
        {
            string FullName = InputHelper.ReadStringValue("Enter host`s fullname: ", v => v.Length > 0,
                "Fullname cannot be null or empty.");

            string PhoneNumber = InputHelper.ReadStringValue("Enter host`s phone number: ",
                v => v.Length > 5 && v.Length < 15, "Phone number cannot be < 5 and > 15 symbols");

            _hosts.Add(new Host(FullName, PhoneNumber));
        }

        public static void ShowHosts()
        {
            if (_hosts.Count > 0)
            {
                for (int i = 0; i < _hosts.Count; i++)
                {
                    Console.Write($"[{i + 1}]. ");
                    _hosts[i].ShowInfo();
                    Console.Write("\n");
                }
            }

            else
                Console.WriteLine("_hosts list is empty!");
        }

        public static void AddApartmentToHost()
        {
            if (_hosts.Count == 0)
            {
                Console.WriteLine("_hosts list is empty!");
                return;
            }
            int hostNumber;

            BookingSystem.ShowHosts();
            Console.Write($"Please, select host number(1 - {_hosts.Count}): ");
            while (true)
            {
                if ((int.TryParse(Console.ReadLine(), out hostNumber) && hostNumber > 0 && hostNumber <= _hosts.Count))
                    break;

                Console.WriteLine("Incorrect host number. Please reenter.");
            }

            string Title = InputHelper.ReadStringValue("Enter apartment Title: ", v => v.Length > 0,
                "Title cannot be null or empty.");

            decimal PricePerNight = InputHelper.ReadDecimalValue("Enter price per night: ", v => v > 0,
                "Please, enter correct decimal value.");

            int Capacity = InputHelper.ReadIntValue("Enter apartment capacity: ", v => v > 0,
                "Invalid apartment capacity. Please enter positive integer value.");

            _hosts[hostNumber - 1].Apartments.Add(new Apartment(Title, PricePerNight, Capacity));
        }


        static int HostNumber;

        public static void ShowApartmentByHostId()
        {
            if (_hosts.Count > 0)
            {
                for (int i = 0; i < _hosts.Count; i++)
                {
                    Console.Write($"[{i + 1}]. ");
                    _hosts[i].ShowInfo();
                    Console.Write("\n");
                }
            }
            else
            {
                Console.WriteLine("There is no _hosts!");
                return;
            }

            HostNumber = InputHelper.ReadIntValue(
                "Please, select host number: ",
                v => v >= 0 && v <= _hosts.Count,
                "Incorrect host number. Please reenter."
            );


            if (_hosts[HostNumber - 1].Apartments.Count == 0)
            {
                Console.WriteLine("This is host without apartments!");
                return;
            }

            for (int i = 0; i < _hosts[HostNumber - 1].Apartments.Count; i++)
            {
                Console.WriteLine(
                    $"{i + 1}. Id: {_hosts[HostNumber - 1].Apartments[i].Id} | Title: {_hosts[HostNumber - 1].Apartments[i].Title} | PricePerNight: {_hosts[HostNumber - 1].Apartments[i].PricePerNight} | Capacity: {_hosts[HostNumber - 1].Apartments[i].Capacity} | IsAvailable: {_hosts[HostNumber - 1].Apartments[i].IsAvailable}");
            }
        }

        public static void BookApartment()
        {
            ShowApartmentByHostId();
            if (_hosts.Count == 0)
                return;

            if (_hosts[HostNumber - 1].Apartments.Count == 0)
                return;

            Console.Write(
                $"Enter {_hosts[HostNumber - 1].FullName} host`s apartment number (1 - {_hosts[HostNumber - 1].Apartments.Count}): ");

            int apartmentNumber;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out apartmentNumber) &&
                    apartmentNumber > 0 &&
                    apartmentNumber <= _hosts[HostNumber - 1].Apartments.Count)
                {
                    break;
                }

                Console.WriteLine("Invalid apartment number. Please reenter.");
            }

            var apartment = _hosts[HostNumber - 1].Apartments[apartmentNumber - 1];

            if (apartment.IsAvailable)
            {
                apartment.IsAvailable = false;
                Console.WriteLine($"Apartment #{apartmentNumber} in {_hosts[HostNumber - 1].FullName} is booked!");
            }
            else
            {
                Console.WriteLine("This apartment was already booked.");
            }
        }
    }
}