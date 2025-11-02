namespace ApartmentBookingApp
{
	internal class Menu
	{
		private HostService _hostService;
		private ApartmentService _apartmentService;

		private int _choice;
		
		public Menu(HostService hostService, ApartmentService apartmentService)
		{
			_hostService = hostService;
			_apartmentService = apartmentService;
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
					_hostService.AddHost();
					break;
				case MenuItem.UpdateHost:
					_hostService.UpdateHost();
					break;
				case MenuItem.DeleteHost:
					_hostService.DeleteHost();
					break;
				case MenuItem.AddApartmentToHost:
					_apartmentService.AddApartmentToHost();
					break;
				case MenuItem.ShowAllHosts:
					_hostService.ShowHosts();
					break;
				case MenuItem.ShowApartmentsByHostId:
					_hostService.ShowHosts();
					if (_hostService.GetHostsCount() == 0)
					{
						break;
					}
					_apartmentService.ShowApartmentsByHostId(ConsoleInputReader.ReadIntValue(
						"Please, select host number: ",
						v => v > 0 && v <= _hostService.GetHostsCount(),
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
