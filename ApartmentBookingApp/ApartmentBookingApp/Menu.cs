namespace ApartmentBookingApp
{
	internal class Menu
	{
		private BookingService _bookingService;
		int _choice;

		readonly List<string> MenuItems = new List<string>
		{
			"Add Host",
			"Add apartment to host",
			"Show all hosts",
			"Show apartments by host id",
			"Exit"
		};

		public Menu(BookingService bookingService)
		{
			_bookingService = bookingService;
		}
		public void ShowMenu()
		{
			Console.Clear();
			Console.WriteLine("Apartment booking menu");
			for (int i = 0; i < MenuItems.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {MenuItems[i]}");
			}
		}

		public void GetUserChoice()
		{
			Console.Write("Enter your choice: ");

			while (_choice == 0)
			{
				if (!int.TryParse(Console.ReadLine(), out _choice) || _choice <= 0 || _choice > MenuItems.Count)
				{
					Console.Write("Reenter your choice: ");
					_choice = 0;
				}
			}
		}

		public void ProcessUserChoice()
		{
			switch (_choice)
			{
				case 1:
					_bookingService.AddHost();
					break;
				case 2:
					_bookingService.AddApartmentToHost();
					break;
				case 3:
					_bookingService.ShowHosts();
					break;
				case 4:
					_bookingService.ShowHosts();
					if (_bookingService.HostsCount() == 0)
					{
						break;
					}
					_bookingService.ShowApartmentByHostId(InputHelper.ReadIntValue(
						"Please, select host number: ",
						v => v > 0 && v <= _bookingService.HostsCount(),
						"Incorrect host number. Please reenter."
					));
					break;
				case 5:
					Environment.Exit(0);
					break;
			}
			_choice = 0;
			Console.Write("Press any key to continue: ");
			Console.ReadKey();
		}
	}
}
