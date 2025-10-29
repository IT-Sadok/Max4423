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
			foreach (MenuItems menuItem in Enum.GetValues(typeof(MenuItems)))
			{
				Console.WriteLine($"{(int)menuItem}. {menuItem}");
			}
		}

		public void GetUserChoice()
		{
			Console.Write("Enter your choice: ");

			while (_choice == 0)
			{
				if (!int.TryParse(Console.ReadLine(), out _choice) || !Enum.IsDefined(typeof(MenuItems), _choice))
				{
					Console.Write("Reenter your choice: ");
					_choice = 0;
				}
			}
		}

		public void ProcessUserChoice()
		{
			switch ((MenuItems)_choice)
			{
				case MenuItems.AddHost:
					_bookingService.AddHost();
					break;
				case MenuItems.AddApartmentToHost:
					_bookingService.AddApartmentToHost();
					break;
				case MenuItems.ShowAllHosts:
					_bookingService.ShowHosts();
					break;
				case MenuItems.ShowApartmentsByHostId:
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
				case MenuItems.Exit:
					Environment.Exit(0);
					break;
			}
			_choice = 0;
			Console.Write("Press any key to continue: ");
			Console.ReadKey();
		}
	}
}
