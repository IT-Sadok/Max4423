namespace ApartmentBookingApp
{
	internal class Menu
	{
		private BookingService _bookingService;
		private int _choice;
		
		public Menu(BookingService bookingService)
		{
			_bookingService = bookingService;
		}
		
		public void ShowMenu()
		{
			Console.Clear();
			Console.WriteLine("Apartment booking menu");
			foreach (MenuItem menuItem in Enum.GetValues(typeof(MenuItem)))
			{
				Console.WriteLine($"{(int)menuItem}. {menuItem}");
			}
		}

		public void GetUserChoice()
		{
			Console.Write("Enter your choice: ");

			while (_choice == 0)
			{
				if (!int.TryParse(Console.ReadLine(), out _choice) || !Enum.IsDefined(typeof(MenuItem), _choice))
				{
					Console.Write("Reenter your choice: ");
					_choice = 0;
				}
			}
		}

		public void ProcessUserChoice()
		{
			switch ((MenuItem)_choice)
			{
				case MenuItem.AddHost:
					_bookingService.AddHost();
					break;
				case MenuItem.AddApartmentToHost:
					_bookingService.AddApartmentToHost();
					break;
				case MenuItem.ShowAllHosts:
					_bookingService.ShowHosts();
					break;
				case MenuItem.ShowApartmentsByHostId:
					_bookingService.ShowHosts();
					if (_bookingService.HostsCount() == 0)
					{
						break;
					}
					_bookingService.ShowApartmentByHostId(ConsoleInputReader.ReadIntValue(
						"Please, select host number: ",
						v => v > 0 && v <= _bookingService.HostsCount(),
						"Incorrect host number. Please reenter."
					));
					break;
				case MenuItem.Exit:
					Environment.Exit(0);
					break;
			}
			_choice = 0;
			Console.Write("Press any key to continue: ");
			Console.ReadKey();
		}
	}
}
