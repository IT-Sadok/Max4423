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

        public void SimulateConcurrentPriceIncrease(int amount)
        {
            Host host1 = new Host() { Id = int.MaxValue - 1, FullName = "Host1", PhoneNumber = "123456789" };
            Host host2 = new Host() { Id = int.MaxValue - 2, FullName = "Host2", PhoneNumber = "123456789" };

            var sharedApartment = new Apartment()
                { Id = int.MaxValue, Title = "shared apartment", PricePerNight = 100, Capacity = 2 };

            host1.Apartments.Add(sharedApartment);
            host2.Apartments.Add(sharedApartment);
            outputWriter.ShowMessage($"Price of shared apartment before Tasks: {sharedApartment.PricePerNight}");

            Task task1 = Task.Run(() => HostIncreasePrice(sharedApartment, amount));
            Task task2 = Task.Run(() => HostIncreasePrice(sharedApartment, amount));

            Task.WaitAll(task1, task2);
            outputWriter.ShowMessage($"Price of shared apartment after Tasks {sharedApartment.PricePerNight}");
        }

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private void HostIncreasePrice(Apartment apartment, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _semaphore.Wait();
                try
                {
                    apartment.PricePerNight++;
                }
                finally
                {
                    _semaphore.Release();
                }
            }
        }
    }
}